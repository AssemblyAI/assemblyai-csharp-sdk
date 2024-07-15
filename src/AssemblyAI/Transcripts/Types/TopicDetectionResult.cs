using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TopicDetectionResult
{
    /// <summary>
    /// The text in the transcript in which a detected topic occurs
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("labels")]
    public IEnumerable<TopicDetectionResultLabelsItem>? Labels { get; init; }

    [JsonPropertyName("timestamp")]
    public Timestamp? Timestamp { get; init; }
}
