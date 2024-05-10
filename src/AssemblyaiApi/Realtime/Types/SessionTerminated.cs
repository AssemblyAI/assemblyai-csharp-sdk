using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class SessionTerminated
{
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }
}
