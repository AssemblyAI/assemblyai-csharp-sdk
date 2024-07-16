using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record SessionTerminated
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; init; }
}
