using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class RealtimeBaseMessage
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; init; }
}
