using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record TranscriptWord
{
    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    [JsonPropertyName("start")]
    public required int Start { get; init; }

    [JsonPropertyName("end")]
    public required int End { get; init; }

    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; init; }
}
