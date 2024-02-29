using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptlabCategoriesResult
{
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; }

    [JsonPropertyName("results")]
    public IEnumerable<TopicDetectionResult> Results { get; init; } = null!;

    [JsonPropertyName("summary")]
    public IReadOnlyDictionary<string, double> Summary { get; init; } = null!;
}