using System.Text.Json.Serialization;

namespace AssemblyAI;

public class WordSearchResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("matches")]
    public IEnumerable<WordSearchMatch> Matches { get; init; }
}