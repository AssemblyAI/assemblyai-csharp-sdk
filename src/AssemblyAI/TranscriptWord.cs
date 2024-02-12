using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptWord
{
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("start")]
    public int Start { get; init; }

    [JsonPropertyName("end")]
    public int End { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("speaker")]
    public string? Speaker { get; init; }
}