using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Lemur;

public record LemurBaseResponse
{
    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public required string RequestId { get; set; }

    /// <summary>
    /// The usage numbers for the LeMUR request
    /// </summary>
    [JsonPropertyName("usage")]
    public required LemurUsage Usage { get; set; }

    /// <summary>
    /// Extra properties that may be returned by the API.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }
    
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
