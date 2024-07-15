using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TranscriptUtterance
{
    /// <summary>
    /// The confidence score for the transcript of this utterance
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    /// <summary>
    /// The starting time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; init; }

    /// <summary>
    /// The ending time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; init; }

    /// <summary>
    /// The text for this utterance
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// The words in the utterance.
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; init; } = new List<TranscriptWord>();

    /// <summary>
    /// The speaker of this utterance, where each speaker is assigned a sequential capital letter - e.g. "A" for Speaker A, "B" for Speaker B, etc.
    /// </summary>
    [JsonPropertyName("speaker")]
    public required string Speaker { get; init; }
}
