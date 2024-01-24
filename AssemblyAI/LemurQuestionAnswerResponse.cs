using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurQuestionAnswerResponse
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = null!;
    
    [JsonPropertyName("response")]
    public List<LemurQuestionAnswer> Response { get; init; } = null!;
}