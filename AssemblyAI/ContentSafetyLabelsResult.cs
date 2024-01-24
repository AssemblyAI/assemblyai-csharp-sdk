using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ContentSafetyLabelsResult
{
    
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; } = null!;
    
    [JsonPropertyName("results")]
    public List<ContentSafetyLabelResult> Results { get; init; } = null!;
    
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; init; } = null!;
    
    [JsonPropertyName("severity_score_summary")]
    public Dictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; init; } = null!;
}