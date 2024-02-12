using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptSentence
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("start")]
    public int Start { get; init; }

    [JsonPropertyName("end")]
    public int End { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; init; } = new List<TranscriptWord>();
}