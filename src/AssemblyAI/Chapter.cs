using System.Text.Json.Serialization;

namespace AssemblyAI;

public class Chapter
{
    [JsonPropertyName("gist")]
    public string Gist { get; init; } = null!;
    
    [JsonPropertyName("headline")]
    public string Headline { get; init; } = null!;
    
    [JsonPropertyName("summary")]
    public string Summary { get; init; } = null!;
    
    [JsonPropertyName("start")] 
    public int Start { get; init; }
    
    [JsonPropertyName("end")] 
    public int End { get; init; }
}