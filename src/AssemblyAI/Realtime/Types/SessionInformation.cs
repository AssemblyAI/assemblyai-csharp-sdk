using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class SessionInformation
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }

    /// <summary>
    /// The total duration of the audio in seconds
    /// </summary>
    [JsonPropertyName("audio_duration_seconds")]
    public double AudioDurationSeconds { get; init; }
}
