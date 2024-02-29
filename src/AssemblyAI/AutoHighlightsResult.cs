using System.Text.Json.Serialization;

namespace AssemblyAI;

public class AutoHighlightsResult
{
    [JsonPropertyName("results")] 
    public IEnumerable<AutoHighlightResult> Results { get; set; }
}