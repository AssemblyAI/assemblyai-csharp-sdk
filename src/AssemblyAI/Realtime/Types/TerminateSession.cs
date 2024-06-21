using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class TerminateSession
{
    /// <summary>
    /// Set to true to end your streaming session forever
    /// </summary>
    [JsonPropertyName("terminate_session")]
    public bool TerminateSession_ { get; init; }
}
