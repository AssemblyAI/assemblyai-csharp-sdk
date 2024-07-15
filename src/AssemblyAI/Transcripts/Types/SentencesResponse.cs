using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record SentencesResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public required double AudioDuration { get; init; }

    [JsonPropertyName("sentences")]
    public IEnumerable<TranscriptSentence> Sentences { get; init; } =
        new List<TranscriptSentence>();
}
