using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class TopicDetectionModelResult
{
    /// <summary>
    /// The status of the Topic Detection model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; }

    /// <summary>
    /// An array of results for the Topic Detection model
    /// </summary>
    [JsonPropertyName("results")]
    public List<TopicDetectionResult> Results { get; init; }

    /// <summary>
    /// The overall relevance of topic to the entire audio file
    /// </summary>
    [JsonPropertyName("summary")]
    public Dictionary<string, double> Summary { get; init; }
}
