using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TopicDetectionResult
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("labels")]
    public IEnumerable<TopicDetectionResultLabelsItem>? Labels { get; init; }

    [JsonPropertyName("timestamp")]
    public Timestamp? Timestamp { get; init; }
}