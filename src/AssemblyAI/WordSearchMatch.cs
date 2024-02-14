using System.Text.Json.Serialization;

namespace AssemblyAI;

public class WordSearchMatch
{
    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("timestamps")]
    public IEnumerable<IEnumerable<int>> Timestamps { get; init; }

    [JsonPropertyName("indexes")]
    public IEnumerable<int> Indexes { get; init; }
}