using System.Text.Json.Serialization;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TopicDetectionResult
{
    /// <summary>
    /// The text in the transcript in which a detected topic occurs
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("labels")]
    public IEnumerable<TopicDetectionResultLabelsItem>? Labels { get; set; }

    [JsonPropertyName("timestamp")]
    public Timestamp? Timestamp { get; set; }
}
