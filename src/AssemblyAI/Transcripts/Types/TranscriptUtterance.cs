using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class TranscriptUtterance
{
    /// <summary>
    /// The confidence score for the transcript of this utterance
    /// </summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    /// <summary>
    /// The starting time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; init; }

    /// <summary>
    /// The ending time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; init; }

    /// <summary>
    /// The text for this utterance
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// The words in the utterance.
    /// </summary>
    [JsonPropertyName("words")]
    public List<TranscriptWord> Words { get; init; }

    /// <summary>
    /// The speaker of this utterance, where each speaker is assigned a sequential capital letter - e.g. "A" for Speaker A, "B" for Speaker B, etc.
    /// </summary>
    [JsonPropertyName("speaker")]
    public string Speaker { get; init; }
}
