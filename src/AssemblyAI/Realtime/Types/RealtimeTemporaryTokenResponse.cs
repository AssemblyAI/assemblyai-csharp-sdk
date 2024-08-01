using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Realtime;

public record RealtimeTemporaryTokenResponse
{
    /// <summary>
    /// The temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}
