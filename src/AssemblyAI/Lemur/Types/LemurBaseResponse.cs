using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class LemurBaseResponse
{
    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; }

    /// <summary>
    /// The usage numbers for the LeMUR request
    /// </summary>
    [JsonPropertyName("usage")]
    public LemurUsage Usage { get; init; }
}
