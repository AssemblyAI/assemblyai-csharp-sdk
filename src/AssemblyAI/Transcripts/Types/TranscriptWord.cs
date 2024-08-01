using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptWord
{
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    [JsonPropertyName("start")]
    public required int Start { get; set; }

    [JsonPropertyName("end")]
    public required int End { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; set; }
}
