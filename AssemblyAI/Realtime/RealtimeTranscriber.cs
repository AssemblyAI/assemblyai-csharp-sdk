using System.ComponentModel;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Websocket.Client;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AssemblyAI.Realtime
{
    public delegate void SessionBeginsEventHandler(RealtimeTranscriber sender, SessionBeginsEventArgs evt);

    public delegate void PartialTranscriptEventHandler(RealtimeTranscriber sender, PartialTranscriptEventArgs evt);

    public delegate void FinalTranscriptEventHandler(RealtimeTranscriber sender, FinalTranscriptEventArgs evt);

    public delegate void TranscriptEventHandler(RealtimeTranscriber sender, TranscriptEventArgs evt);

    public delegate void ErrorEventHandler(RealtimeTranscriber sender, ErrorEventArgs evt);

    public delegate void ClosedEventHandler(RealtimeTranscriber sender, ClosedEventArgs evt);

    public enum RealtimeTranscriberStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting
    }

    public sealed class RealtimeTranscriber : IAsyncDisposable, IDisposable, INotifyPropertyChanged
    {
        private const string RealtimeServiceEndpoint = "wss://api.assemblyai.com/v2/realtime/ws";
        private WebsocketClient _socket;
        private TaskCompletionSource<bool> _sessionTerminatedTaskCompletionSource;
        private CancellationTokenSource _listenerCancellationTokenSource;

        /// <summary>
        /// Use your AssemblyAI API key to authenticate with the AssemblyAI real-time transcriber.
        /// </summary>
        public string ApiKey { private get; set; }

        /// <summary>
        /// Use a temporary auth token to authenticate with the AssemblyAI real-time transcriber.
        /// Learn <see href="https://www.assemblyai.com/docs/guides/real-time-streaming-transcription#creating-temporary-authentication-tokens">how to generate a temporary token here</see>.
        /// </summary>
        public string Token { private get; set; }

        /// <summary>
        /// The sample rate of the streamed audio. Defaults to 16000.
        /// </summary>
        public uint SampleRate { get; set; } = 16_000;

        /// <summary>
        /// Add up to 2500 characters of custom vocabulary
        /// </summary>
        public IEnumerable<string> WordBoost { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// The encoding of the audio data
        /// </summary>
        public string Encoding { get; set; }


        private RealtimeTranscriberStatus _status;

        public RealtimeTranscriberStatus Status
        {
            get => _status;
            private set => SetField(ref _status, value);
        }

        /// <summary>
        /// Event for when the real-time session begins
        /// </summary>
        public event SessionBeginsEventHandler SessionBegins;

        /// <summary>
        /// Event for when a partial transcript is received.
        /// </summary>
        public event PartialTranscriptEventHandler PartialTranscriptReceived;

        private Subject<PartialRealtimeTranscript> _partialTranscripts = new();
        
        /// <summary>
        /// 
        /// </summary>
        public IObservable<PartialRealtimeTranscript> PartialTranscripts => _partialTranscripts; 

        /// <summary>
        /// Event for when a final transcript is received.
        /// </summary>
        public event FinalTranscriptEventHandler FinalTranscriptReceived;

        private Subject<FinalRealtimeTranscript> _finalTranscripts = new();
        public IObservable<FinalRealtimeTranscript> FinalTranscripts => _finalTranscripts; 

        /// <summary>
        /// Event for when a partial or final transcript is received.
        /// </summary>
        public event TranscriptEventHandler TranscriptReceived;

        private Subject<RealtimeTranscript> _transcripts = new();
        public IObservable<RealtimeTranscript> Transcripts => _transcripts; 

        /// <summary>
        /// Event for when an error is received from the real-time service.
        /// </summary>
        public event ErrorEventHandler ErrorReceived;

        /// <summary>
        /// Event for when the connection with the real-time service is closed.
        /// </summary>
        public event ClosedEventHandler Closed;

        /// <summary>
        /// Connect to AssemblyAI's real-time transcription service, and start listening for messages.
        /// </summary>
        /// <returns>The session begins message</returns>
        public async Task<SessionBeginsMessage> ConnectAsync()
        {
            if (Status != RealtimeTranscriberStatus.Disconnected)
            {
                throw new Exception($"Transcriber is already in status {Status}");
            }

            if (string.IsNullOrEmpty(Token) && string.IsNullOrEmpty(ApiKey))
            {
                throw new Exception("You must configure ApiKey or Token to authenticate the real-time transcriber.");
            }

            var urlBuilder = new StringBuilder(RealtimeServiceEndpoint);
            var queryPrefix = "?";
            if (SampleRate > 0)
            {
                urlBuilder.AppendFormat("?sample_rate={0}", SampleRate);
                queryPrefix = "&";
            }

            if (WordBoost.Any())
            {
                urlBuilder.AppendFormat("{0}word_boost={1}", queryPrefix, JsonSerializer.Serialize(WordBoost));
                queryPrefix = "&";
            }

            if (!string.IsNullOrEmpty(Encoding))
            {
                urlBuilder.AppendFormat("{0}encoding={1}", queryPrefix, Encoding);
                queryPrefix = "&";
            }

            if (_socket != null && _socket.IsRunning)
            {
                _socket.Dispose();
            }

            if (!string.IsNullOrEmpty(Token))
            {
                urlBuilder.AppendFormat("{0}token={1}", queryPrefix, Token);
            }

            _socket = new WebsocketClient(new Uri(urlBuilder.ToString()), () =>
            {
                var socket = new ClientWebSocket();
                if (string.IsNullOrEmpty(Token))
                {
                    socket.Options.SetRequestHeader("Authorization", ApiKey);
                }

                return socket;
            });

            Status = RealtimeTranscriberStatus.Connecting;
            try
            {
                await _socket.StartOrFail();
                Status = RealtimeTranscriberStatus.Connected;
            }
            catch (Exception)
            {
                Status = RealtimeTranscriberStatus.Disconnected;
                throw;
            }

            _socket.DisconnectionHappened.Subscribe(d =>
            {
                OnClosed((int)d.CloseStatus, d.CloseStatusDescription);
            });

            var sessionBeginsTaskCompletionSource = new TaskCompletionSource<SessionBeginsMessage>();
            _socket.MessageReceived
                .Select(message => JsonSerializer.Deserialize<JsonDocument>(message.Text!))
                .Subscribe(message =>
                {
                    if (message.RootElement.TryGetProperty("error", out var errorProperty))
                    {
                        var error = errorProperty.GetString();
                        OnErrorReceived(error);
                        if (sessionBeginsTaskCompletionSource.Task.IsCompleted == false)
                        {
                            sessionBeginsTaskCompletionSource.SetException(new Exception(error));
                        }
                    }

                    if (!message.RootElement.TryGetProperty("message_type", out var messageTypeProperty)) return;
                    
                    var messageType = messageTypeProperty.GetString();
                    switch (messageType)
                    {
                        case "SessionBegins":
                            var sessionBeginsMessage = message.Deserialize<SessionBeginsMessage>();
                            OnSessionBegins(sessionBeginsMessage);
                            sessionBeginsTaskCompletionSource.SetResult(sessionBeginsMessage);
                            break;
                        case "PartialTranscript":
                        {
                            var partialTranscript = message.Deserialize<PartialRealtimeTranscript>();
                            OnPartialTranscriptReceived(partialTranscript);
                            var transcript = message.Deserialize<RealtimeTranscript>();
                            OnTranscriptReceived(transcript);
                            break;
                        }
                        case "FinalTranscript":
                        {
                            var finalTranscript = message.Deserialize<FinalRealtimeTranscript>();
                            OnFinalTranscriptReceived(finalTranscript);
                            var transcript = message.Deserialize<RealtimeTranscript>();
                            OnTranscriptReceived(transcript);
                            break;
                        }
                        case "SessionTerminated":
                            OnSessionTerminated();
                            break;
                    }
                });
            
            _listenerCancellationTokenSource = new CancellationTokenSource();
            return await sessionBeginsTaskCompletionSource.Task;
        }

        /// <summary>
        /// Called when session begins message is received. Calls the SessionBegins event.
        /// </summary>
        /// <param name="sessionBeginsMessage"></param>
        private void OnSessionBegins(SessionBeginsMessage sessionBeginsMessage)
        {
            RaiseEvent(SessionBegins, this, new SessionBeginsEventArgs(sessionBeginsMessage));
        }

        /// <summary>
        /// Called when a partial transcript message is received. Calls the PartialTranscriptReceived event.
        /// </summary>
        /// <param name="realtimeTranscript"></param>
        private void OnPartialTranscriptReceived(PartialRealtimeTranscript realtimeTranscript)
        {
            _partialTranscripts.OnNext(realtimeTranscript);
            RaiseEvent(PartialTranscriptReceived, this, new PartialTranscriptEventArgs(realtimeTranscript));
        }

        /// <summary>
        /// Called when a final transcript message is received. Calls the FinalTranscriptReceived event.
        /// </summary>
        /// <param name="realtimeTranscript"></param>
        private void OnFinalTranscriptReceived(FinalRealtimeTranscript realtimeTranscript)
        {
            _finalTranscripts.OnNext(realtimeTranscript);
            RaiseEvent(FinalTranscriptReceived, this, new FinalTranscriptEventArgs(realtimeTranscript));
        }

        /// <summary>
        /// Called when a partial or a final transcript message is received. Calls the TranscriptReceived event.
        /// </summary>
        /// <param name="realtimeTranscript"></param>
        private void OnTranscriptReceived(RealtimeTranscript realtimeTranscript)
        {
            _transcripts.OnNext(realtimeTranscript);
            RaiseEvent(TranscriptReceived, this, new TranscriptEventArgs(realtimeTranscript));
        }

        /// <summary>
        /// Called when the session terminated message is received. Completes the session terminated task.
        /// </summary>
        private void OnSessionTerminated()
        {
            _sessionTerminatedTaskCompletionSource?.TrySetResult(true);
        }

        private void OnClosed(int code, string reason)
        {
            Status = RealtimeTranscriberStatus.Disconnected;
            RaiseEvent(Closed, this, new ClosedEventArgs
            {
                Code = code,
                Reason = reason
            });
        }

        /// <summary>
        /// Called when an error message is received. Calls the ErrorReceived event.
        /// </summary>
        /// <param name="error"></param>
        private void OnErrorReceived(string error)
        {
            Status = RealtimeTranscriberStatus.Disconnected;
            RaiseEvent(ErrorReceived, this, new ErrorEventArgs
            {
                Error = error
            });
        }

        /// <summary>
        /// Send audio to the real-time service.
        /// </summary>
        /// <param name="audio">Audio to transcribe</param>
        /// <returns></returns>
        public void SendAudio(ArraySegment<byte> audio) => _socket.Send(audio);


        /// <summary>
        /// Send audio to the real-time service.
        /// </summary>
        /// <param name="audio">Audio to transcribe</param>
        public void SendAudio(byte[] audio) => _socket.Send(audio);

        /// <summary>
        /// Terminates the real-time transcription session, closes the connection, and disposes the real-time transcriber.
        /// </summary>
        /// <remarks>
        /// Any remaining partial or final transcripts will not be received.
        /// To receive remaining partial or final transcripts, call <see cref="CloseAsync()" /> before disposing.
        /// </remarks>
        public void Dispose()
        {
            if (_socket.IsRunning)
            {
                CloseAsync(false).Wait();
            }

            _socket.Dispose();
            RemoveEvents();
        }

        /// <summary>
        /// Terminates the real-time transcription session, closes the connection, and disposes the real-time transcriber.
        /// </summary>
        /// <remarks>
        /// Any remaining partial or final transcripts will not be received.
        /// To receive remaining partial or final transcripts, call <see cref="CloseAsync()" /> before disposing.
        /// </remarks>
        public async ValueTask DisposeAsync()
        {
            if (_socket.IsRunning)
            {
                await CloseAsync(false);
            }

            _socket.Dispose();
            RemoveEvents();
        }

        private void RemoveEvents()
        {
            SessionBegins = null;
            PartialTranscriptReceived = null;
            FinalTranscriptReceived = null;
            TranscriptReceived = null;
            Closed = null;
            ErrorReceived = null;
            PropertyChanged = null;
        }

        /// <summary>
        /// Terminate the real-time transcription session and close the connection.
        /// </summary>
        /// <returns></returns>
        public Task CloseAsync() => CloseAsync(true);

        /// <summary>
        /// Terminate the real-time transcription session and close the connection.
        /// </summary>
        /// <param name="waitForSessionTerminated">Wait to receive pending transcripts and session terminated message.</param>
        public async Task CloseAsync(bool waitForSessionTerminated)
        {
            Status = RealtimeTranscriberStatus.Disconnecting;
            var ct = CancellationToken.None;
            if (waitForSessionTerminated)
            {
                _sessionTerminatedTaskCompletionSource = new TaskCompletionSource<bool>();
            }

            _socket.Send("{\"terminate_session\": true}");
            if (waitForSessionTerminated)
            {
                await _sessionTerminatedTaskCompletionSource.Task
                    .ConfigureAwait(false);
            }

            await _socket.StopOrFail(WebSocketCloseStatus.NormalClosure, "");

            Status = RealtimeTranscriberStatus.Disconnected;
            _listenerCancellationTokenSource.Cancel();
            
        }

        private void DisposeObservables()
        {
            _partialTranscripts.Dispose();
            _finalTranscripts.Dispose();
            _transcripts.Dispose();
        }

        private static void RaiseEvent(MulticastDelegate evt, params object[] args)
        {
            if (evt is null) return;

            foreach (var d in evt.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke { InvokeRequired: true } syncer)
                {
                    syncer.EndInvoke(syncer.BeginInvoke(d, args));
                }
                else
                {
                    d.DynamicInvoke(args);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    /// <summary>
    /// The newly started real-time transcription session.
    /// </summary>
    public sealed class SessionBeginsMessage
    {
        [JsonPropertyName("session_id")] public Guid SessionId { get; set; }
        [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
    }

    public class RealtimeTranscriberEventArgs<T> : EventArgs
    {
        internal RealtimeTranscriberEventArgs(T result)
        {
            Result = result;
        }

        public T Result { get; }
    }

    /// <summary>
    /// Event arguments for the newly started real-time transcription session.
    /// </summary>
    public sealed class SessionBeginsEventArgs : RealtimeTranscriberEventArgs<SessionBeginsMessage>
    {
        internal SessionBeginsEventArgs(SessionBeginsMessage result) : base(result)
        {
        }
    }

    /// <summary>
    /// Event arguments for a partial transcript.
    /// </summary>
    public sealed class PartialTranscriptEventArgs : RealtimeTranscriberEventArgs<PartialRealtimeTranscript>
    {
        internal PartialTranscriptEventArgs(PartialRealtimeTranscript result) : base(result)
        {
        }
    }


    /// <summary>
    /// Event arguments for a final transcript.
    /// </summary>
    public sealed class FinalTranscriptEventArgs : RealtimeTranscriberEventArgs<FinalRealtimeTranscript>
    {
        internal FinalTranscriptEventArgs(FinalRealtimeTranscript result) : base(result)
        {
        }
    }

    /// <summary>
    /// Event arguments for a partial or final partial transcript.
    /// </summary>
    public sealed class TranscriptEventArgs : RealtimeTranscriberEventArgs<RealtimeTranscript>
    {
        internal TranscriptEventArgs(RealtimeTranscript result) : base(result)
        {
        }
    }

    /// <summary>
    /// A final or partial transcript
    /// </summary>
    public class RealtimeTranscript
    {
        [JsonPropertyName("text")] public string Text { get; set; }
        [JsonPropertyName("words")] public IEnumerable<Word> Words { get; set; }
    }

    public class Word
    {
        [JsonPropertyName("start")] public int Start { get; set; }

        [JsonPropertyName("text")] public string Text { get; set; }
    }

    /// <summary>
    /// A final transcript
    /// </summary>
    public class FinalRealtimeTranscript : RealtimeTranscript
    {
    }

    /// <summary>
    /// A partial transcript
    /// </summary>
    public class PartialRealtimeTranscript : RealtimeTranscript
    {
    }

    /// <summary>
    /// Event arguments for an error sent by the real-time service.
    /// </summary>
    public sealed class ErrorEventArgs : EventArgs
    {
        internal ErrorEventArgs()
        {
        }

        public string Error { get; set; }
    }

    /// <summary>
    /// Event arguments for when the connection with the real-time service is closed.
    /// </summary>
    public sealed class ClosedEventArgs : EventArgs
    {
        internal ClosedEventArgs()
        {
        }

        public int Code { get; internal set; }
        public string Reason { get; internal set; }
    }
}