using System.Text.Json.Serialization;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TopicDetectionModelResult
{
    /// <summary>
    /// The status of the Topic Detection model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public required AudioIntelligenceModelStatus Status { get; set; }

    /// <summary>
    /// An array of results for the Topic Detection model
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<TopicDetectionResult> Results { get; set; } =
        new List<TopicDetectionResult>();

    /// <summary>
    /// The overall relevance of topic to the entire audio file
    /// </summary>
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; set; } = new Dictionary<string, double>();
}
