using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class ContentSafetyLabelsResult
{
    /// <summary>
    /// The status of the Content Moderation model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; }

    [JsonPropertyName("results")]
    public IEnumerable<ContentSafetyLabelResult> Results { get; init; }

    /// <summary>
    /// A summary of the Content Moderation confidence results for the entire audio file
    /// </summary>
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; init; }

    /// <summary>
    /// A summary of the Content Moderation severity results for the entire audio file
    /// </summary>
    [JsonPropertyName("severity_score_summary")]
    public Dictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; init; }
}
