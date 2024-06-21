using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class RealtimeError
{
    [JsonPropertyName("error")]
    public string Error { get; init; }
}
