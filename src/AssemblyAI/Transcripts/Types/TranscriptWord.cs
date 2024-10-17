using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptWord
{
    /// <summary>
    /// The confidence score for the transcript of this word
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, for the word
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The ending time, in milliseconds, for the word
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

    /// <summary>
    /// The text of the word
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The channel of the word. The left and right channels are channels 1 and 2. Additional channels increment the channel number sequentially.
    /// </summary>
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    /// <summary>
    /// The speaker of the word if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
