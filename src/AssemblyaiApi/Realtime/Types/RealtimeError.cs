using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class RealtimeError
{
    [JsonPropertyName("error")]
    public string Error { get; init; }
}
