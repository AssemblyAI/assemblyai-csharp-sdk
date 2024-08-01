using System.Text.Json.Serialization;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

public record SentencesResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    [JsonPropertyName("audio_duration")]
    public required double AudioDuration { get; set; }

    [JsonPropertyName("sentences")]
    public IEnumerable<TranscriptSentence> Sentences { get; set; } = new List<TranscriptSentence>();
}
