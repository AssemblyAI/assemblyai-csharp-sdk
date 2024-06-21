using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class SentencesResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("audio_duration")]
    public double AudioDuration { get; init; }

    [JsonPropertyName("sentences")]
    public IEnumerable<TranscriptSentence> Sentences { get; init; }
}
