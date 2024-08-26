using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Lemur;

public record PurgeLemurRequestDataResponse
{
    /// <summary>
    /// The ID of the deletion request of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public required string RequestId { get; set; }

    /// <summary>
    /// The ID of the LeMUR request to purge the data for
    /// </summary>
    [JsonPropertyName("request_id_to_purge")]
    public required string RequestIdToPurge { get; set; }

    /// <summary>
    /// Whether the request data was deleted
    /// </summary>
    [JsonPropertyName("deleted")]
    public required bool Deleted { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
