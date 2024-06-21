using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class Word
{
    /// <summary>
    /// Start time of the word in milliseconds
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; init; }

    /// <summary>
    /// End time of the word in milliseconds
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; init; }

    /// <summary>
    /// Confidence score of the word
    /// </summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    /// <summary>
    /// The word itself
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }
}
