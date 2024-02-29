using System.Text.Json.Serialization;

namespace AssemblyAI;

public class PurgeLemurRequestDataResponse
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = null!;

    [JsonPropertyName("request_id_to_purge")]
    public string RequestIdToPurge { get; init; } = null!;

    [JsonPropertyName("deleted")]
    public bool Deleted { get; init; }
}