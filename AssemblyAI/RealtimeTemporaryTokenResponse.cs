using System.Text.Json.Serialization;

namespace AssemblyAI;

public class RealtimeTemporaryTokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; init; } = null!;
}