using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record ContentSafetyLabelsResult
{
    /// <summary>
    /// The status of the Content Moderation model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public required AudioIntelligenceModelStatus Status { get; init; }

    [JsonPropertyName("results")]
    public IEnumerable<ContentSafetyLabelResult> Results { get; init; } =
        new List<ContentSafetyLabelResult>();

    /// <summary>
    /// A summary of the Content Moderation confidence results for the entire audio file
    /// </summary>
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; init; } = new Dictionary<string, double>();

    /// <summary>
    /// A summary of the Content Moderation severity results for the entire audio file
    /// </summary>
    [JsonPropertyName("severity_score_summary")]
    public Dictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; init; } =
        new Dictionary<string, SeverityScoreSummary>();
}
