using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record AutoHighlightsResult
{
    /// <summary>
    /// The status of the Key Phrases model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public required AudioIntelligenceModelStatus Status { get; set; }

    /// <summary>
    /// A temporally-sequential array of Key Phrases
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<AutoHighlightResult> Results { get; set; } = new List<AutoHighlightResult>();
}
