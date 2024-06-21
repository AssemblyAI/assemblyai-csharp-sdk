using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class WordSearchMatch
{
    /// <summary>
    /// The matched word
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// The total amount of times the word is in the transcript
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }

    /// <summary>
    /// An array of timestamps
    /// </summary>
    [JsonPropertyName("timestamps")]
    public IEnumerable<IEnumerable<int>> Timestamps { get; init; }

    /// <summary>
    /// An array of all index locations for that word within the `words` array of the completed transcript
    /// </summary>
    [JsonPropertyName("indexes")]
    public IEnumerable<int> Indexes { get; init; }
}
