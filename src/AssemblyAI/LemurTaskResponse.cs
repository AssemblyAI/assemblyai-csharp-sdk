using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurTaskResponse
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = null!;
    
    [JsonPropertyName("response")]
    public string Response { get; init; } = null!;
}