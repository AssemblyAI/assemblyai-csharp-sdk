using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ContentSafetyLabel
{
    [JsonPropertyName("label")]
    public string Label { get; init; } = null!;
    
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }
    
    [JsonPropertyName("severity")]
    public double Severity { get; init; } 
}