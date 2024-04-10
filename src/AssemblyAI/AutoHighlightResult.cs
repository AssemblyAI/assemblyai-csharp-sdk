using System.Text.Json.Serialization;

namespace AssemblyAI;

public class AutoHighlightResult
{
    [JsonPropertyName("count")] 
    public int Count { get; init; }

    [JsonPropertyName("rank")] 
    public double Rank { get; init; }

    [JsonPropertyName("text")] 
    public string Text { get; init; } = null!;

    [JsonPropertyName("timestamps")] 
    public IEnumerable<Timestamp> Timestamps { get; init; } = null!;
}