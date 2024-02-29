using System.Text.Json.Serialization;

namespace AssemblyAI;

public class SessionTerminated
{
    [JsonPropertyName("message_type")]
    public string MessageType = "SessionTerminated";
}