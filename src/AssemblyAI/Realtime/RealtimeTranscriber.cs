using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebSocket = AssemblyAI.Realtime.WebsocketClient.WebsocketClient;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

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
    private readonly RealtimeTranscriberOptions _options;
    private WebSocket? _socket;
    private TaskCompletionSource<SessionInformation>? _sessionTerminatedTaskCompletionSource;
    private RealtimeTranscriberStatus _status;

    public RealtimeTranscriberStatus Status
    {
        get => _status;
        private set => SetField(ref _status, value);
    }

    /// <summary>
    /// The Streaming STT endpoint to connect to. 
    /// </summary>
    public string RealtimeUrl
    {
        get => _options.RealtimeUrl;
        set => _options.RealtimeUrl = value;
    }

    /// <summary>
    /// Use your AssemblyAI API key to authenticate with the AssemblyAI real-time transcriber.
    /// </summary>
    public string? ApiKey
    {
        internal get => _options.ApiKey;
        set => _options.ApiKey = value;
    }

    /// <summary>
    /// Use a temporary auth token to authenticate with the AssemblyAI real-time transcriber.
    /// Learn <see href="https://www.assemblyai.com/docs/guides/real-time-streaming-transcription#creating-temporary-authentication-tokens">how to generate a temporary token here</see>.
    /// </summary>
    public string? Token
    {
        internal get => _options.Token;
        set => _options.Token = value;
    }

    /// <summary>
    /// The sample rate of the streamed audio. Defaults to 16000.
    /// </summary>
    public uint SampleRate
    {
        get => _options.SampleRate;
        set => _options.SampleRate = value;
    }

    /// <summary>
    /// Add up to 2500 characters of custom vocabulary
    /// </summary>
    public IEnumerable<string> WordBoost
    {
        get => _options.WordBoost;
        set => _options.WordBoost = value;
    }

    /// <summary>
    /// The encoding of the audio data
    /// </summary>
    public AudioEncoding? Encoding
    {
        get => _options.Encoding;
        set => _options.Encoding = value;
    }

    /// <summary>
    /// Disable partial transcripts.
    /// Set to `true` to not receive partial transcripts. Defaults to `false`.
    /// </summary>
    public bool DisablePartialTranscripts
    {
        get => _options.DisablePartialTranscripts;
        set => _options.DisablePartialTranscripts = value;
    }

    public readonly Event<SessionBegins> SessionBegins = new();
    public readonly Event<PartialTranscript> PartialTranscriptReceived = new();
    public readonly Event<FinalTranscript> FinalTranscriptReceived = new();
    public readonly Event<RealtimeTranscript> TranscriptReceived = new();
    public readonly Event<SessionInformation> SessionInformationReceived = new();
    public readonly Event<RealtimeError> ErrorReceived = new();
    public readonly Event<Exception> ExceptionOccurred = new();
    public readonly Event<ClosedEventArgs> Closed = new();
    private SessionInformation? _sessionInformation;

    public RealtimeTranscriber()
    {
        _options = new RealtimeTranscriberOptions();
    }

    public RealtimeTranscriber(RealtimeTranscriberOptions options)
    {
        _options = options;
    }

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

        var urlBuilder = new StringBuilder(RealtimeUrl);
        urlBuilder.Append($"?sample_rate={SampleRate}");

        if (DisablePartialTranscripts)
        {
            urlBuilder.Append("&disable_partial_transcripts=true");
        }

        urlBuilder.Append("&enable_extra_session_information=true");

        if (WordBoost.Any())
        {
            urlBuilder.Append(
                $"&word_boost={UrlEncoder.Default.Encode(JsonSerializer.Serialize(WordBoost))}"
            );
        }

        if (Encoding != null)
        {
            urlBuilder.Append($"&encoding={EnumConverter.ToString(Encoding.Value)}");
        }

        if (!string.IsNullOrEmpty(Token))
        {
            urlBuilder.Append($"&token={UrlEncoder.Default.Encode(Token!)}");
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
        _socket.ExceptionOccurred = OnExceptionOccurred;
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

    private readonly Dictionary<int, string> _closeCodeErrorMessages = new()
    {
        { 4000, "Sample rate must be a positive integer" },
        { 4001, "Not Authorized" },
        { 4002, "Insufficient Funds" },
        {
            4003,
            "This feature is paid-only and requires you to add a credit card. Please visit https://app.assemblyai.com/ to add a credit card to your account."
        },
        { 4004, "Session ID does not exist" },
        { 4008, "Session has expired" },
        { 4010, "Session is closed" },
        { 4029, "Rate limited" },
        { 4030, "Unique session violation" },
        { 4031, "Session Timeout" },
        { 4032, "Audio too short" },
        { 4033, "Audio too long" },
        { 4034, "Audio too small to transcode" },
        { 4101, "Bad schema" },
        { 4102, "Too many streams" },
        { 4103, "Reconnected" },
        { 4104, "Could not parse word boost parameter" }
    };

    private async Task OnClosed(int? code, string? reason)
    {
        Status = RealtimeTranscriberStatus.Disconnected;
        if (
            code != null &&
            string.IsNullOrEmpty(reason) &&
            _closeCodeErrorMessages.TryGetValue(code.Value, out var message))
        {
            reason = message;
        }

        await Closed.RaiseEvent(new ClosedEventArgs
        {
            Code = code,
            Reason = reason
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when an error message is received. Calls the ErrorReceived event.
    /// </summary>
    /// <param name="error">The error sent by the realtime service.</param>
    private async Task OnError(RealtimeError error)
    {
        Status = RealtimeTranscriberStatus.Disconnected;
        await ErrorReceived.RaiseEvent(error).ConfigureAwait(false);
    }

    /// <summary>
    /// Called when an exception is thrown while the transcriber listens for WebSocket messages.
    /// </summary>
    /// <param name="exception">The exception</param>
    private async Task OnExceptionOccurred(Exception exception)
    {
        await ExceptionOccurred.RaiseEvent(exception).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Send audio to the real-time service.
    /// </summary>
    /// <param name="audio">Audio to transcribe</param>
    public Task SendAudioAsync(Memory<byte> audio)
    {
        if (_status != RealtimeTranscriberStatus.Connected)
        {
            throw new Exception($"Cannot send audio when status is {_status.ToString()}");
        }

        return _socket!.SendInstant(audio);
    }

    /// <summary>
    /// Send audio to the real-time service.
    /// </summary>
    /// <param name="audio">Audio to transcribe</param>
    public Task SendAudioAsync(ArraySegment<byte> audio)
    {
        if (_status != RealtimeTranscriberStatus.Connected)
        {
            throw new Exception($"Cannot send audio when status is {_status.ToString()}");
        }

        return _socket!.SendInstant(audio);
    }

    /// <summary>
    /// Send audio to the real-time service.
    /// </summary>
    /// <param name="audio">Audio to transcribe</param>
    public Task SendAudioAsync(byte[] audio)
    {
        if (_status != RealtimeTranscriberStatus.Connected)
        {
            throw new Exception($"Cannot send audio when status is {_status.ToString()}");
        }

        return _socket!.SendInstant(audio);
    }

    /// <summary>
    /// Manually end an utterance
    /// </summary>
    public Task ForceEndUtteranceAsync()
    {
        return _socket!.SendInstant("{\"force_end_utterance\":true}");
    }
    
    /// <summary>
    /// Configure the threshold for how long to wait before ending an utterance. Default is 700ms.
    /// </summary>
    /// <param name="threshold">
    /// The duration of the end utterance silence threshold in milliseconds.
    /// This value must be an integer between 0 and 20_000.
    /// </param>
    public Task ConfigureEndUtteranceThresholdAsync(uint threshold)
    {
        return _socket!.SendInstant($"{{\"end_utterance_silence_threshold\":{threshold}}}");
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

        await _socket!.SendInstant("{\"terminate_session\": true}").ConfigureAwait(false);
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