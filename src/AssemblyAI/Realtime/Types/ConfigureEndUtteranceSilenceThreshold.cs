using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class ConfigureEndUtteranceSilenceThreshold
{
    /// <summary>
    /// The duration threshold in milliseconds
    /// </summary>
    [JsonPropertyName("end_utterance_silence_threshold")]
    public int EndUtteranceSilenceThreshold { get; init; }
}
