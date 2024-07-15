using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record RealtimeTemporaryTokenResponse
{
    /// <summary>
    /// The temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; init; }
}
