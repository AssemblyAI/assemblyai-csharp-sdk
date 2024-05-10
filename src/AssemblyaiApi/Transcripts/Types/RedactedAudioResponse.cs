using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class RedactedAudioResponse
{
    /// <summary>
    /// The status of the redacted audio
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; init; }

    /// <summary>
    /// The URL of the redacted audio file
    /// </summary>
    [JsonPropertyName("redacted_audio_url")]
    public string RedactedAudioURL { get; init; }
}
