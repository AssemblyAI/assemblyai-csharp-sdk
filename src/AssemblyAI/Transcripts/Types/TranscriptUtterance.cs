using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptUtterance
{
    /// <summary>
    /// The confidence score for the transcript of this utterance
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The ending time, in milliseconds, of the utterance in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

    /// <summary>
    /// The text for this utterance
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The words in the utterance.
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; set; } = new List<TranscriptWord>();

    /// <summary>
    /// The channel of this utterance. The left and right channels are channels 1 and 2. Additional channels increment the channel number sequentially.
    /// </summary>
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    /// <summary>
    /// The speaker of this utterance, where each speaker is assigned a sequential capital letter - e.g. "A" for Speaker A, "B" for Speaker B, etc.
    /// </summary>
    [JsonPropertyName("speaker")]
    public required string Speaker { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
