using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record RedactedAudioResponse
{
    /// <summary>
    /// The status of the redacted audio
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// The URL of the redacted audio file
    /// </summary>
    [JsonPropertyName("redacted_audio_url")]
    public required string RedactedAudioUrl { get; set; }
}
