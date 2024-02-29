using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TopicDetectionResultLabelsItem
{
    [JsonPropertyName("relevance")]
    public double Relevance { get; init; }

    [JsonPropertyName("label")]
    public string Label { get; init; } = null!;
}