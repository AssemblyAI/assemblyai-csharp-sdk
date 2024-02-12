using System.Text.Json.Serialization;

namespace AssemblyAI;

public class ParagraphsResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = null!;

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public double AudioDuration { get; init; }

    [JsonPropertyName("paragraphs")]
    public IEnumerable<TranscriptParagraph> Paragraphs { get; init; } = null!;
}

