using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TranscriptSentence
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("start")]
    public required int Start { get; init; }

    [JsonPropertyName("end")]
    public required int End { get; init; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; init; } = new List<TranscriptWord>();

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; init; }
}
