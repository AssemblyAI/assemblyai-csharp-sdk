using System.Text.Json.Serialization;

namespace AssemblyAI;

public class Timestamp
{
    [JsonPropertyName("start")] 
    public int Start { get; init; }

    [JsonPropertyName("end")] 
    public int End { get; init; }
}