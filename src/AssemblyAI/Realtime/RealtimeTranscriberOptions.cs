namespace AssemblyAI.Realtime;

/// <summary>
/// The options for the real-time transcriber.
/// </summary>
public sealed class RealtimeTranscriberOptions
{
    /// <summary>
    /// The Streaming STT endpoint to connect to. 
    /// </summary>
    public string RealtimeUrl { get; set; } = "wss://api.assemblyai.com/v2/realtime/ws";

    /// <summary>
    /// Use your AssemblyAI API key to authenticate with the AssemblyAI real-time transcriber.
    /// </summary>
    public string? ApiKey { internal get; set; }

    /// <summary>
    /// Use a temporary auth token to authenticate with the AssemblyAI real-time transcriber.
    /// Learn <see href="https://www.assemblyai.com/docs/guides/real-time-streaming-transcription#creating-temporary-authentication-tokens">how to generate a temporary token here</see>.
    /// </summary>
    public string? Token { internal get; set; }

    /// <summary>
    /// The sample rate of the streamed audio. Defaults to 16000.
    /// </summary>
    public uint SampleRate { get; set; } = 16_000;

    /// <summary>
    /// Add up to 2500 characters of custom vocabulary
    /// </summary>
    public IEnumerable<string> WordBoost { get; set; } = [];

    /// <summary>
    /// The encoding of the audio data
    /// </summary>
    public AudioEncoding? Encoding { get; set; }

    /// <summary>
    /// Disable partial transcripts.
    /// Set to `true` to not receive partial transcripts. Defaults to `false`.
    /// </summary>
    public bool DisablePartialTranscripts { get; set; }
}