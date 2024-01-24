using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurBaseResponse
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = null!;
}