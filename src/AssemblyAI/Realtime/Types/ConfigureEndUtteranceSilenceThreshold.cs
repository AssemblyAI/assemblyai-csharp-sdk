using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Realtime;

public record ConfigureEndUtteranceSilenceThreshold
{
    /// <summary>
    /// The duration threshold in milliseconds
    /// </summary>
    [JsonPropertyName("end_utterance_silence_threshold")]
    public required int EndUtteranceSilenceThreshold { get; set; }
}
