using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptParagraph
{
    /// <summary>
    /// The transcript of the paragraph
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, of the paragraph
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The ending time, in milliseconds, of the paragraph
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

    /// <summary>
    /// The confidence score for the transcript of this paragraph
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// An array of words in the paragraph
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; set; } = new List<TranscriptWord>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
