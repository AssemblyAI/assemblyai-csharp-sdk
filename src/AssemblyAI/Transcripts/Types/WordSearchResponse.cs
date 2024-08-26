using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record WordSearchResponse
{
    /// <summary>
    /// The ID of the transcript
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The total count of all matched instances. For e.g., word 1 matched 2 times, and word 2 matched 3 times, `total_count` will equal 5.
    /// </summary>
    [JsonPropertyName("total_count")]
    public required int TotalCount { get; set; }

    /// <summary>
    /// The matches of the search
    /// </summary>
    [JsonPropertyName("matches")]
    public IEnumerable<WordSearchMatch> Matches { get; set; } = new List<WordSearchMatch>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
