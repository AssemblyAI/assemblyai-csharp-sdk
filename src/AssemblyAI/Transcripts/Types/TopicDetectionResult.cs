using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class TopicDetectionResult
{
    /// <summary>
    /// The text in the transcript in which a detected topic occurs
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("labels")]
    public List<TopicDetectionResultLabelsItem>? Labels { get; init; }

    [JsonPropertyName("timestamp")]
    public Timestamp? Timestamp { get; init; }
}
