using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class SessionInformation
{
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }

    /// <summary>
    /// The total duration of the audio in seconds
    /// </summary>
    [JsonPropertyName("audio_duration_seconds")]
    public double AudioDurationSeconds { get; init; }
}
