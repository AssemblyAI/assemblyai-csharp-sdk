using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class ParagraphsResponse
{
    [JsonPropertyName("id")]
    public string ID { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public double AudioDuration { get; init; }

    [JsonPropertyName("paragraphs")]
    public List<TranscriptParagraph> Paragraphs { get; init; }
}
