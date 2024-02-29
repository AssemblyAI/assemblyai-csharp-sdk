using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ContentSafetyLabelResult
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("labels")]
    public IEnumerable<ContentSafetyLabel> labels { get; init; } = null!;

    [JsonPropertyName("sentences_idx_start")]
    public int SentencesIdxStart { get; init; }

    [JsonPropertyName("sentences_idx_end")]
    public int SentencesIdxEnd { get; init; }

    [JsonPropertyName("timestamp")]
    public Timestamp Timestamp { get; init; } = null!;
}