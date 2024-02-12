using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ErrorParams
{
    [JsonPropertyName("error")]
    public string Error { get; init; } = null!;
    
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}