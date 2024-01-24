using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TerminateSession : RealtimeBaseMessage
{
    [JsonPropertyName("terminateSession")]
    public bool TerminateSession_ { get; init; }
}