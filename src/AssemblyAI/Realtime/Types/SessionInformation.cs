using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record SessionInformation
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; init; }

    /// <summary>
    /// The total duration of the audio in seconds
    /// </summary>
    [JsonPropertyName("audio_duration_seconds")]
    public required double AudioDurationSeconds { get; init; }
}
