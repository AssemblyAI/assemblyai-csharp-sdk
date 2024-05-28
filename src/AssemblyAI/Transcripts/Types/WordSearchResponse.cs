using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class WordSearchResponse
{
    /// <summary>
    /// The ID of the transcript
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The total count of all matched instances. For e.g., word 1 matched 2 times, and word 2 matched 3 times, `total_count` will equal 5.
    /// </summary>
    [JsonPropertyName("total_count")]
    public int TotalCount { get; init; }

    /// <summary>
    /// The matches of the search
    /// </summary>
    [JsonPropertyName("matches")]
    public List<WordSearchMatch> Matches { get; init; }
}
