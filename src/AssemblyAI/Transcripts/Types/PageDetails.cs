using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record PageDetails
{
    /// <summary>
    /// The number of results this page is limited to
    /// </summary>
    [JsonPropertyName("limit")]
    public required int Limit { get; init; }

    /// <summary>
    /// The actual number of results in the page
    /// </summary>
    [JsonPropertyName("result_count")]
    public required int ResultCount { get; init; }

    /// <summary>
    /// The URL used to retrieve the current page of transcripts
    /// </summary>
    [JsonPropertyName("current_url")]
    public required string CurrentUrl { get; init; }

    /// <summary>
    /// The URL to the next page of transcripts. The previous URL always points to a page with older transcripts.
    /// </summary>
    [JsonPropertyName("prev_url")]
    public string? PrevUrl { get; init; }

    /// <summary>
    /// The URL to the next page of transcripts. The next URL always points to a page with newer transcripts.
    /// </summary>
    [JsonPropertyName("next_url")]
    public string? NextUrl { get; init; }
}
