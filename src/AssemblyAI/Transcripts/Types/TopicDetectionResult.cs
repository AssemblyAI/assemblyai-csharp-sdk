using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TopicDetectionResult
{
    /// <summary>
    /// The text in the transcript in which a detected topic occurs
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// An array of detected topics in the text
    /// </summary>
    [JsonPropertyName("labels")]
    public IEnumerable<TopicDetectionResultLabelsItem>? Labels { get; set; }

    [JsonPropertyName("timestamp")]
    public Timestamp? Timestamp { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
