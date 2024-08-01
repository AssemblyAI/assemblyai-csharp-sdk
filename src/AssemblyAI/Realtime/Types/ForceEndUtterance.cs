using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Realtime;

public record ForceEndUtterance
{
    /// <summary>
    /// A boolean value to communicate that you wish to force the end of the utterance
    /// </summary>
    [JsonPropertyName("force_end_utterance")]
    public required bool ForceEndUtterance_ { get; set; }
}
