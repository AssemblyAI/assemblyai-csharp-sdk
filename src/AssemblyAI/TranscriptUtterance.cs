using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptUtterance
{
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("start")]
    public int Start { get; init; }

    [JsonPropertyName("end")]
    public int End { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; init; } = null!;

    [JsonPropertyName("speaker")]
    public string Speaker { get; init; } = null!;
}