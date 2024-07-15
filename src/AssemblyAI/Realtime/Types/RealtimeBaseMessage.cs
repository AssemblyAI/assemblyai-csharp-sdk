using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record RealtimeBaseMessage
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required MessageType MessageType { get; init; }
}
