using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Realtime;

public record RealtimeError
{
    [JsonPropertyName("error")]
    public required string Error { get; set; }
}
