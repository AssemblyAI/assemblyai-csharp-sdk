using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class ContentSafetyLabelResult
{
    /// <summary>
    /// The transcript of the section flagged by the Content Moderation model
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// An array of safety labels, one per sensitive topic that was detected in the section
    /// </summary>
    [JsonPropertyName("labels")]
    public IEnumerable<ContentSafetyLabel> Labels { get; init; }

    /// <summary>
    /// The sentence index at which the section begins
    /// </summary>
    [JsonPropertyName("sentences_idx_start")]
    public int SentencesIdxStart { get; init; }

    /// <summary>
    /// The sentence index at which the section ends
    /// </summary>
    [JsonPropertyName("sentences_idx_end")]
    public int SentencesIdxEnd { get; init; }

    /// <summary>
    /// Timestamp information for the section
    /// </summary>
    [JsonPropertyName("timestamp")]
    public Timestamp Timestamp { get; init; }
}
