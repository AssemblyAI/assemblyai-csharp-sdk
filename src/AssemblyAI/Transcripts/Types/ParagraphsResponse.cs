using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record ParagraphsResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    [JsonPropertyName("audio_duration")]
    public required double AudioDuration { get; set; }

    [JsonPropertyName("paragraphs")]
    public IEnumerable<TranscriptParagraph> Paragraphs { get; set; } =
        new List<TranscriptParagraph>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
