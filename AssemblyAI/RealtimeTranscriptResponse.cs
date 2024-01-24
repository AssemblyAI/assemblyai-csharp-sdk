using System.Text.Json.Serialization;

namespace AssemblyAI;

public class RealtimeTranscriptResponse
{
    [JsonPropertyName("status")]
    public string Status { get; init; } = null!;

    [JsonPropertyName("redacted_audio_url")]
    public string RedactedAudioUrl { get; init; } = null!;
}