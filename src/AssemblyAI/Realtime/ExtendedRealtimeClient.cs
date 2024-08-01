#nullable enable

using AssemblyAI.Core;

namespace AssemblyAI.Realtime;

public partial class RealtimeClient
{
    /// <summary>
    /// Create a temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    public Task<RealtimeTemporaryTokenResponse> CreateTemporaryTokenAsync(
        int expiresIn,
        RequestOptions? options = null
    )
        => CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams
        {
            ExpiresIn = expiresIn
        });

    /// <summary>
    /// Create a real-time transcriber
    /// </summary>
    /// <returns>The real-time transcriber</returns>
    public RealtimeTranscriber Transcriber() => Transcriber(new RealtimeTranscriberOptions());

    /// <summary>
    /// Create a real-time transcriber with options
    /// </summary>
    /// <param name="options">Options for the real-time transcriber</param>
    /// <returns>The real-time transcriber</returns>
    public RealtimeTranscriber Transcriber(RealtimeTranscriberOptions options)
    {
        if (options.Token == null && options.ApiKey == null)
        {
            options.ApiKey = _client.Options.ApiKey;
        }

        var transcriber = new RealtimeTranscriber(options);
        return transcriber;
    }
}