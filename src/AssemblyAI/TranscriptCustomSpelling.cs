using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TranscriptCustomSpelling
{
    [JsonPropertyName("from")]
    public IEnumerable<string> From { get; init; }
    
    [JsonPropertyName("to")]
    public string To { get; init; }
}