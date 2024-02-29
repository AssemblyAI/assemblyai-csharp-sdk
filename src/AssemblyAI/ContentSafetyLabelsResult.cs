using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ContentSafetyLabelsResult
{

    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; } = null!;

    [JsonPropertyName("results")]
    public IEnumerable<ContentSafetyLabelResult> Results { get; init; } = null!;

    [JsonPropertyName("summary")]
    public IReadOnlyDictionary<string, double> Summary { get; init; } = null!;

    [JsonPropertyName("severity_score_summary")]
    public IReadOnlyDictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; init; } = null!;
}