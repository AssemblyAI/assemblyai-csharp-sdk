using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using WebSocket = AssemblyAI.Realtime.WebsocketClient.WebsocketClient;

namespace AssemblyAI.Realtime;

public enum RealtimeTranscriberStatus
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting
}

public sealed class RealtimeTranscriber : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private WebSocket? _socket;
    private TaskCompletionSource<SessionInformation>? _sessionTerminatedTaskCompletionSource;

    public string Endpoint { get; set; } = "wss://localhost:10000/v2/realtime/ws";

    /// <summary>
    /// Use your AssemblyAI API key to authenticate with the AssemblyAI real-time transcriber.
    /// </summary>
    public string? ApiKey { private get; set; }

    /// <summary>
    /// Use a temporary auth token to authenticate with the AssemblyAI real-time transcriber.
    /// Learn <see href="https://www.assemblyai.com/docs/guides/real-time-streaming-transcription#creating-temporary-authentication-tokens">how to generate a temporary token here</see>.
    /// </summary>
    public string? Token { private get; set; }

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
    public string? Encoding { get; set; }

    /// <summary>
    /// Disable partial transcripts.
    /// Set to `true` to not receive partial transcripts. Defaults to `false`.
    /// </summary>
    public bool DisablePartialTranscripts { get; set; }

    private RealtimeTranscriberStatus _status;

    public RealtimeTranscriberStatus Status
    {
        get => _status;
        private set => SetField(ref _status, value);
    }

    public readonly Event<SessionBegins> SessionBegins = new();
    public readonly Event<PartialTranscript> PartialTranscriptReceived = new();
    public readonly Event<FinalTranscript> FinalTranscriptReceived = new();
    public readonly Event<RealtimeTranscript> TranscriptReceived = new();
    public readonly Event<SessionInformation> SessionInformationReceived = new();
    public readonly Event<RealtimeError> ErrorReceived = new();
    public readonly Event<ClosedEventArgs> Closed = new();
    private SessionInformation? _sessionInformation;

    /// <summary>
    /// Connect to AssemblyAI's real-time transcription service, and start listening for messages.
    /// </summary>
    /// <returns>The session begins message</returns>
    public async Task<SessionBegins> ConnectAsync()
    {
        if (Status != RealtimeTranscriberStatus.Disconnected)
        {
            throw new Exception($"Transcriber status is {Status}");
        }

        if (string.IsNullOrEmpty(Token) && string.IsNullOrEmpty(ApiKey))
        {
            throw new Exception("You must configure ApiKey or Token to authenticate the real-time transcriber.");
        }

        var urlBuilder = new StringBuilder(Endpoint);
        urlBuilder.AppendFormat("?sample_rate={0}", SampleRate);

        if (DisablePartialTranscripts)
        {
            urlBuilder.Append("&disable_partial_transcripts=true");
        }

        urlBuilder.Append("&enable_extra_session_information=true");

        if (WordBoost.Any())
        {
            urlBuilder.AppendFormat("&word_boost={0}", JsonSerializer.Serialize(WordBoost));
        }

        if (!string.IsNullOrEmpty(Encoding))
        {
            urlBuilder.AppendFormat("&encoding={0}", Encoding);
        }

        if (!string.IsNullOrEmpty(Token))
        {
            urlBuilder.AppendFormat("&token={0}", Token);
        }

        _socket?.Dispose();

        var sessionBeginsTaskCompletionSource = new TaskCompletionSource<SessionBegins>();
        _socket = new WebSocket(new Uri(urlBuilder.ToString()), () =>
        {
            var socket = new ClientWebSocket();
            if (string.IsNullOrEmpty(Token))
            {
                socket.Options.SetRequestHeader("Authorization", ApiKey);
            }

            return socket;
        });
        _socket.TextMessageReceived = async stream =>
        {
            var message = await JsonSerializer.DeserializeAsync<JsonDocument>(stream);
            if (message == null) return;
            if (message.RootElement.TryGetProperty("error", out var errorProperty))
            {
                var error = errorProperty.GetString();
                await OnError(new RealtimeError { Error = error! })
                    .ConfigureAwait(false);
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
                    var sessionBeginsMessage = message.Deserialize<SessionBegins>();
                    sessionBeginsTaskCompletionSource.SetResult(sessionBeginsMessage!);
                    await OnSessionBegins(sessionBeginsMessage!).ConfigureAwait(false);
                    break;
                case "PartialTranscript":
                {
                    var partialTranscript = message.Deserialize<PartialTranscript>();
                    await OnPartialTranscript(partialTranscript!).ConfigureAwait(false);
                    await OnTranscript(partialTranscript!).ConfigureAwait(false);
                    break;
                }
                case "FinalTranscript":
                {
                    var finalTranscript = message.Deserialize<FinalTranscript>();
                    await OnFinalTranscript(finalTranscript!).ConfigureAwait(false);
                    await OnTranscript(finalTranscript!).ConfigureAwait(false);
                    break;
                }
                case "SessionInformation":
                    var sessionInformation = message.Deserialize<SessionInformation>();
                    await OnSessionInformation(sessionInformation!).ConfigureAwait(false);
                    break;
                case "SessionTerminated":
                    OnSessionTerminated();
                    break;
            }
        };

        Status = RealtimeTranscriberStatus.Connecting;
        try
        {
            await _socket.StartOrFail().ConfigureAwait(false);
            Status = RealtimeTranscriberStatus.Connected;
        }
        catch (Exception)
        {
            Status = RealtimeTranscriberStatus.Disconnected;
            throw;
        }

        _socket.DisconnectionHappened = async d => { await OnClosed((int?)d.CloseStatus, d.CloseStatusDescription); };

        return await sessionBeginsTaskCompletionSource.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Called when session begins message is received. Calls the SessionBegins event.
    /// </summary>
    /// <param name="sessionBeginsMessage"></param>
    private async Task OnSessionBegins(SessionBegins sessionBeginsMessage)
    {
        await SessionBegins.RaiseEvent(sessionBeginsMessage).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when a partial transcript message is received. Calls the PartialTranscriptReceived event.
    /// </summary>
    /// <param name="partialTranscript"></param>
    private async Task OnPartialTranscript(PartialTranscript partialTranscript)
    {
        await PartialTranscriptReceived.RaiseEvent(partialTranscript).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when a final transcript message is received. Calls the FinalTranscriptReceived event.
    /// </summary>
    /// <param name="finalTranscript"></param>
    private async Task OnFinalTranscript(FinalTranscript finalTranscript)
    {
        await FinalTranscriptReceived.RaiseEvent(finalTranscript).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when a partial or a final transcript message is received. Calls the TranscriptReceived event.
    /// </summary>
    /// <param name="realtimeTranscript"></param>
    private async Task OnTranscript(RealtimeTranscript realtimeTranscript)
    {
        await TranscriptReceived.RaiseEvent(realtimeTranscript).ConfigureAwait(false);
    }

    /// <summary>
    /// This message is sent at the end of the session, before the SessionTerminated message.
    /// </summary>
    /// <param name="sessionInformation"></param>
    private async Task OnSessionInformation(SessionInformation sessionInformation)
    {
        _sessionInformation = sessionInformation;
        await SessionInformationReceived.RaiseEvent(sessionInformation).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when the session terminated message is received. Completes the session terminated task.
    /// </summary>
    private void OnSessionTerminated()
    {
        _sessionTerminatedTaskCompletionSource?.TrySetResult(_sessionInformation!);
    }

    private async Task OnClosed(int? code, string reason)
    {
        Status = RealtimeTranscriberStatus.Disconnected;
        await Closed.RaiseEvent(new ClosedEventArgs
        {
            Code = code,
            Reason = reason
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when an error message is received. Calls the ErrorReceived event.
    /// </summary>
    /// <param name="error"></param>
    private async Task OnError(RealtimeError error)
    {
        Status = RealtimeTranscriberStatus.Disconnected;
        await ErrorReceived.RaiseEvent(error).ConfigureAwait(false);
    }

    /// <summary>
    /// Send audio to the real-time service.
    /// </summary>
    /// <param name="audio">Audio to transcribe</param>
    /// <returns></returns>
    public void SendAudio(ArraySegment<byte> audio)
    {
        if (_status != RealtimeTranscriberStatus.Connected)
        {
            throw new Exception($"Cannot send audio when status is {_status.ToString()}");
        }

        _socket!.Send(audio);
    }

    /// <summary>
    /// Send audio to the real-time service.
    /// </summary>
    /// <param name="audio">Audio to transcribe</param>
    public void SendAudio(byte[] audio)
    {
        if (_status != RealtimeTranscriberStatus.Connected)
        {
            throw new Exception($"Cannot send audio when status is {_status.ToString()}");
        }

        _socket!.Send(audio);
    }

    /// <summary>
    /// Terminates the real-time transcription session, closes the connection, and disposes the real-time transcriber.
    /// </summary>
    /// <remarks>
    /// Any remaining partial or final transcripts will not be received.
    /// To receive remaining partial or final transcripts, call <see cref="CloseAsync()" /> before disposing.
    /// </remarks>
    public void Dispose()
    {
        if (_socket != null)
        {
            if (_socket.IsRunning)
            {
                CloseAsync(false).Wait();
            }

            _socket.Dispose();
        }

        DisposeEvents();
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
        if (_socket != null)
        {
            if (_socket.IsRunning)
            {
                await CloseAsync(false).ConfigureAwait(false);
            }

            _socket.Dispose();
            _socket = null;
        }

        DisposeEvents();
    }

    private void DisposeEvents()
    {
        SessionBegins.Dispose();
        PartialTranscriptReceived.Dispose();
        FinalTranscriptReceived.Dispose();
        TranscriptReceived.Dispose();
        ErrorReceived.Dispose();
        Closed.Dispose();
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
        if (waitForSessionTerminated)
        {
            _sessionTerminatedTaskCompletionSource = new TaskCompletionSource<SessionInformation>();
        }

        _socket!.Send("{\"terminate_session\": true}");
        if (waitForSessionTerminated)
        {
            await _sessionTerminatedTaskCompletionSource!.Task
                .ConfigureAwait(false);
        }

        await _socket.StopOrFail(WebSocketCloseStatus.NormalClosure, "");

        Status = RealtimeTranscriberStatus.Disconnected;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // ReSharper disable once UnusedMethodReturnValue.Local
    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

/// <summary>
/// Event arguments for when the connection with the real-time service is closed.
/// </summary>
public sealed class ClosedEventArgs
{
    public int? Code { get; internal set; }
    public string? Reason { get; internal set; }
}