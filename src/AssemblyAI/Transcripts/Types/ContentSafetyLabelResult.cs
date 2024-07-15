using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record ContentSafetyLabelResult
{
    /// <summary>
    /// The transcript of the section flagged by the Content Moderation model
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// An array of safety labels, one per sensitive topic that was detected in the section
    /// </summary>
    [JsonPropertyName("labels")]
    public IEnumerable<ContentSafetyLabel> Labels { get; init; } = new List<ContentSafetyLabel>();

    /// <summary>
    /// The sentence index at which the section begins
    /// </summary>
    [JsonPropertyName("sentences_idx_start")]
    public required int SentencesIdxStart { get; init; }

    /// <summary>
    /// The sentence index at which the section ends
    /// </summary>
    [JsonPropertyName("sentences_idx_end")]
    public required int SentencesIdxEnd { get; init; }

    /// <summary>
    /// Timestamp information for the section
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required Timestamp Timestamp { get; init; }
}
