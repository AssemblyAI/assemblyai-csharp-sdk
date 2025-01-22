using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record ContentSafetyLabelsResult
{
    /// <summary>
    /// The status of the Content Moderation model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; set; }

    /// <summary>
    /// An array of results for the Content Moderation model
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<ContentSafetyLabelResult> Results { get; set; } =
        new List<ContentSafetyLabelResult>();

    /// <summary>
    /// A summary of the Content Moderation confidence results for the entire audio file
    /// </summary>
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; set; } = new Dictionary<string, double>();

    /// <summary>
    /// A summary of the Content Moderation severity results for the entire audio file
    /// </summary>
    [JsonPropertyName("severity_score_summary")]
    public Dictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; set; } =
        new Dictionary<string, SeverityScoreSummary>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
