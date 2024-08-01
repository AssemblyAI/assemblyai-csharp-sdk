using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TopicDetectionResultLabelsItem
{
    /// <summary>
    /// How relevant the detected topic is of a detected topic
    /// </summary>
    [JsonPropertyName("relevance")]
    public required double Relevance { get; set; }

    /// <summary>
    /// The IAB taxonomical label for the label of the detected topic, where > denotes supertopic/subtopic relationship
    /// </summary>
    [JsonPropertyName("label")]
    public required string Label { get; set; }
}
