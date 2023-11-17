using System.Text.Json.Serialization;

namespace AssemblyAI;

public class SentencesResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = null!;

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public double AudioDuration { get; init; }

    [JsonPropertyName("sentences")]
    public List<TranscriptSentence> Sentences { get; init; } = new List<TranscriptSentence>();
}