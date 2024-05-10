using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class ConfigureEndUtteranceSilenceThreshold
{
    /// <summary>
    /// The duration threshold in milliseconds
    /// </summary>
    [JsonPropertyName("end_utterance_silence_threshold")]
    public int EndUtteranceSilenceThreshold { get; init; }
}
