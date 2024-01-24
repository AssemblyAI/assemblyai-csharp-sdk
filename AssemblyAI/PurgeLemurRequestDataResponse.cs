using System.Text.Json.Serialization;

namespace AssemblyAI;

public class PurgeLemurRequestDataResponse
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; init; } = null!;

    [JsonPropertyName("requestIdToPurge")]
    public string RequestIdToPurge { get; init; } = null!;

    [JsonPropertyName("deleted")]
    public bool Deleted { get; init; }
}