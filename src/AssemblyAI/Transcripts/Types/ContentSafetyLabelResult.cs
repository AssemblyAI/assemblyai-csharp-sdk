using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record ContentSafetyLabelResult
{
    /// <summary>
    /// The transcript of the section flagged by the Content Moderation model
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// An array of safety labels, one per sensitive topic that was detected in the section
    /// </summary>
    [JsonPropertyName("labels")]
    public IEnumerable<ContentSafetyLabel> Labels { get; set; } = new List<ContentSafetyLabel>();

    /// <summary>
    /// The sentence index at which the section begins
    /// </summary>
    [JsonPropertyName("sentences_idx_start")]
    public required int SentencesIdxStart { get; set; }

    /// <summary>
    /// The sentence index at which the section ends
    /// </summary>
    [JsonPropertyName("sentences_idx_end")]
    public required int SentencesIdxEnd { get; set; }

    /// <summary>
    /// Timestamp information for the section
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required Timestamp Timestamp { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
