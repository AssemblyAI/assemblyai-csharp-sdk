using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record PageDetails
{
    /// <summary>
    /// The number of results this page is limited to
    /// </summary>
    [JsonPropertyName("limit")]
    public required int Limit { get; set; }

    /// <summary>
    /// The actual number of results in the page
    /// </summary>
    [JsonPropertyName("result_count")]
    public required int ResultCount { get; set; }

    /// <summary>
    /// The URL used to retrieve the current page of transcripts
    /// </summary>
    [JsonPropertyName("current_url")]
    public required string CurrentUrl { get; set; }

    /// <summary>
    /// The URL to the next page of transcripts. The previous URL always points to a page with older transcripts.
    /// </summary>
    [JsonPropertyName("prev_url")]
    public string? PrevUrl { get; set; }

    /// <summary>
    /// The URL to the next page of transcripts. The next URL always points to a page with newer transcripts.
    /// </summary>
    [JsonPropertyName("next_url")]
    public string? NextUrl { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
