using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptCustomSpelling
{
    [JsonPropertyName("from")]
    public List<string> From { get; init; }
    
    [JsonPropertyName("to")]
    public string To { get; init; }
}