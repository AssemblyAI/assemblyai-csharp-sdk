using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class SessionTerminated
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }
}
