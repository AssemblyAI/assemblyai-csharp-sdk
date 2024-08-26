using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

public record SessionInformation
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; set; }

    /// <summary>
    /// The total duration of the audio in seconds
    /// </summary>
    [JsonPropertyName("audio_duration_seconds")]
    public required float AudioDurationSeconds { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
