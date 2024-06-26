using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class AutoHighlightsResult
{
    /// <summary>
    /// The status of the Key Phrases model. Either success, or unavailable in the rare case that the model failed.
    /// </summary>
    [JsonPropertyName("status")]
    public AudioIntelligenceModelStatus Status { get; init; }

    /// <summary>
    /// A temporally-sequential array of Key Phrases
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<AutoHighlightResult> Results { get; init; }
}
