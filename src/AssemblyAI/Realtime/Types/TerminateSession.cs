using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

public record TerminateSession
{
    /// <summary>
    /// Set to true to end your streaming session forever
    /// </summary>
    [JsonPropertyName("terminate_session")]
    public required bool TerminateSession_ { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
