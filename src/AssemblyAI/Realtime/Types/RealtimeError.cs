using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record RealtimeError
{
    [JsonPropertyName("error")]
    public required string Error { get; init; }
}
