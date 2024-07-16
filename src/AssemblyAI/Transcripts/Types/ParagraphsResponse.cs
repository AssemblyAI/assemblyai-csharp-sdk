using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record ParagraphsResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public required double AudioDuration { get; init; }

    [JsonPropertyName("paragraphs")]
    public IEnumerable<TranscriptParagraph> Paragraphs { get; init; } =
        new List<TranscriptParagraph>();
}
