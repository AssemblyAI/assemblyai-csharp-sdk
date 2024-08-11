using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptSentence
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("start")]
    public required int Start { get; set; }

    [JsonPropertyName("end")]
    public required int End { get; set; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord> Words { get; set; } = new List<TranscriptWord>();

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; set; }
}
