using System.Text.Json.Serialization;

namespace AssemblyAI.Transcripts
{
    public sealed class TranscriptIEnumerableRequest
    {
        [JsonPropertyName("limit")] 
        public int? Limit { get; set; }
    
        [JsonPropertyName("created_on")] 
        public string CreatedOn { get; set; }
    
        [JsonPropertyName("before_id")] 
        public string BeforeId { get; set; }
    
        [JsonPropertyName("after_id")] 
        public string AfterId { get; set; }
    
        [JsonPropertyName("throttled_only")] 
        public bool? ThrottledOnly { get; set; }
    }   
}