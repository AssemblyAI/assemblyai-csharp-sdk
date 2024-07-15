using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record AutoHighlightResult
{
    /// <summary>
    /// The total number of times the key phrase appears in the audio file
    /// </summary>
    [JsonPropertyName("count")]
    public required int Count { get; init; }

    /// <summary>
    /// The total relevancy to the overall audio file of this key phrase - a greater number means more relevant
    /// </summary>
    [JsonPropertyName("rank")]
    public required double Rank { get; init; }

    /// <summary>
    /// The text itself of the key phrase
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// The timestamp of the of the key phrase
    /// </summary>
    [JsonPropertyName("timestamps")]
    public IEnumerable<Timestamp> Timestamps { get; init; } = new List<Timestamp>();
}
