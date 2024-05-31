using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class TopicDetectionResultLabelsItem
{
    /// <summary>
    /// How relevant the detected topic is of a detected topic
    /// </summary>
    [JsonPropertyName("relevance")]
    public double Relevance { get; init; }

    /// <summary>
    /// The IAB taxonomical label for the label of the detected topic, where > denotes supertopic/subtopic relationship
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; init; }
}
