using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record WordSearchMatch
{
    /// <summary>
    /// The matched word
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// The total amount of times the word is in the transcript
    /// </summary>
    [JsonPropertyName("count")]
    public required int Count { get; init; }

    /// <summary>
    /// An array of timestamps
    /// </summary>
    [JsonPropertyName("timestamps")]
    public IEnumerable<IEnumerable<int>> Timestamps { get; init; } = new List<IEnumerable<int>>();

    /// <summary>
    /// An array of all index locations for that word within the `words` array of the completed transcript
    /// </summary>
    [JsonPropertyName("indexes")]
    public IEnumerable<int> Indexes { get; init; } = new List<int>();
}
