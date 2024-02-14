using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class PurgeLemurRequestDataResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        [JsonPropertyName("request_id_to_purge")]
        public string RequestIdToPurge { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }
    }
}