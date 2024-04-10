using System.Text.Json.Serialization;

namespace AssemblyAI.Realtime;

public class SessionInformation
{
    /// <summary>
    /// The total duration of the audio in seconds
    /// </summary>
    [JsonPropertyName("audio_duration_seconds")]
    public float AudioDurationSeconds { get; set; }

    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; }
}