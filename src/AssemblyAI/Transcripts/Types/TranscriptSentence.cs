using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptSentence
{
    /// <summary>
    /// The transcript of the sentence
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, for the sentence
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The ending time, in milliseconds, for the sentence
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

    /// <summary>
    /// The confidence score for the transcript of this sentence
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// An array of words in the sentence
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; set; } = new List<TranscriptWord>();

    /// <summary>
    /// The channel of the sentence. The left and right channels are channels 1 and 2. Additional channels increment the channel number sequentially.
    /// </summary>
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
