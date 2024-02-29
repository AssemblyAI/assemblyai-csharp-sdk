using System.Text.Json.Serialization;

namespace AssemblyAI;

public class Word
{
    [JsonPropertyName("start")]
    public int Start { get; init; }

    [JsonPropertyName("end")]
    public int End { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;
}