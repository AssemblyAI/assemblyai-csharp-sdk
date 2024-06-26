using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class PurgeLemurRequestDataResponse
{
    /// <summary>
    /// The ID of the deletion request of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; }

    /// <summary>
    /// The ID of the LeMUR request to purge the data for
    /// </summary>
    [JsonPropertyName("request_id_to_purge")]
    public string RequestIdToPurge { get; init; }

    /// <summary>
    /// Whether the request data was deleted
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; init; }
}
