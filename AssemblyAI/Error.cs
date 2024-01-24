using System.Text.Json.Serialization;

namespace AssemblyAI;

public class Error
{
    [JsonPropertyName("error")]
    public string Error_ { get; init; } = null!;
    
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}