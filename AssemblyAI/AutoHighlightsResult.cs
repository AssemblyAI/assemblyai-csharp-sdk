using System.Text.Json.Serialization;

namespace AssemblyAI;

public class AutoHighlightsResult
{
    [JsonPropertyName("results")] 
    public List<AutoHighlightResult> Results { get; init; } = null!;
}