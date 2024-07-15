using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record Word
{
    /// <summary>
    /// Start time of the word in milliseconds
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; init; }

    /// <summary>
    /// End time of the word in milliseconds
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; init; }

    /// <summary>
    /// Confidence score of the word
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; init; }

    /// <summary>
    /// The word itself
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
