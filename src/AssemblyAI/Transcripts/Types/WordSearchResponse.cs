using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record WordSearchResponse
{
    /// <summary>
    /// The ID of the transcript
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The total count of all matched instances. For e.g., word 1 matched 2 times, and word 2 matched 3 times, `total_count` will equal 5.
    /// </summary>
    [JsonPropertyName("total_count")]
    public required int TotalCount { get; init; }

    /// <summary>
    /// The matches of the search
    /// </summary>
    [JsonPropertyName("matches")]
    public IEnumerable<WordSearchMatch> Matches { get; init; } = new List<WordSearchMatch>();
}
