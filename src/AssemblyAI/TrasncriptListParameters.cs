using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TrasncriptIEnumerableParameters
    {
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        [JsonPropertyName("status")]
        public TranscriptStatus Status { get; set; }

        [JsonPropertyName("createdOn")]
        public string CreatedOn { get; set; }

        [JsonPropertyName("beforeId")]
        public string BeforeId { get; set; }

        [JsonPropertyName("afterId")]
        public string AfterId { get; set; }

        [JsonPropertyName("throttledOnly")]
        public bool? ThrottledOnly { get; set; }
    }
}