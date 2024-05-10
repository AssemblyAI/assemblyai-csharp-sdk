using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class RealtimeTemporaryTokenResponse
{
    /// <summary>
    /// The temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; init; }
}
