using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class PageDetails
{
    /// <summary>
    /// The number of results this page is limited to
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; init; }

    /// <summary>
    /// The actual number of results in the page
    /// </summary>
    [JsonPropertyName("result_count")]
    public int ResultCount { get; init; }

    /// <summary>
    /// The URL used to retrieve the current page of transcripts
    /// </summary>
    [JsonPropertyName("current_url")]
    public string CurrentURL { get; init; }

    /// <summary>
    /// The URL to the next page of transcripts. The previous URL always points to a page with older transcripts.
    /// </summary>
    [JsonPropertyName("prev_url")]
    public string? PrevURL { get; init; }

    /// <summary>
    /// The URL to the next page of transcripts. The next URL always points to a page with newer transcripts.
    /// </summary>
    [JsonPropertyName("next_url")]
    public string? NextURL { get; init; }
}
