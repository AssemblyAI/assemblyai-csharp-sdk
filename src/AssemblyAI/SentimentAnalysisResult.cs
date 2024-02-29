using System.Text.Json.Serialization;

namespace AssemblyAI;

public class SentimentAnalysisResult
{
    [JsonPropertyName("text")] 
    public string Text { get; init; } = null!;

    [JsonPropertyName("start")]
    public int Start { get; init; }

    [JsonPropertyName("end")]
    public int End { get; init; }

    [JsonPropertyName("sentiment")]
    public Sentiment Sentiment { get; init; }  = null!;

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("speaker")]
    public string? Speaker { get; init; }
}