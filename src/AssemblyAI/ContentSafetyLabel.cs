using System.Text.Json.Serialization;

namespace AssemblyAI;

public partial class ContentSafetyLabel
{
    [JsonPropertyName("label")] public string Label = null;

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("severity")]
    public double Severity { get; init; } 
}