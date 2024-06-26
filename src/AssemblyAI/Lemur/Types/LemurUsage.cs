using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class LemurUsage
{
    /// <summary>
    /// The number of input tokens used by the model
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; init; }

    /// <summary>
    /// The number of output tokens generated by the model
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; init; }
}
