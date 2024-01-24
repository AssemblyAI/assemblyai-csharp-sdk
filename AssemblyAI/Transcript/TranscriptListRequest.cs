using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptListRequest
    {
        [JsonPropertyName("limit")] 
        public int? Limit { get; init; }
    
        [JsonPropertyName("created_on")] 
        public string? CreatedOn { get; init; }
    
        [JsonPropertyName("before_id")] 
        public string? BeforeId { get; init; }
    
        [JsonPropertyName("after_id")] 
        public string? AfterId { get; init; }
    
        [JsonPropertyName("throttled_only")] 
        public bool? ThrottledOnly { get; init; }
    }   
}