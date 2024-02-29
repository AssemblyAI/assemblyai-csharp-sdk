using System.Text.Json.Serialization;

namespace AssemblyAI.Realtime;

public class CreateRealtimeTemporaryTokenParameters
{
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
}