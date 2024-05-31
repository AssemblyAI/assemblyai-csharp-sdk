using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class LemurBaseResponse
{
    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; }
}
