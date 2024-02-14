using System.Text.Json.Serialization;

namespace AssemblyAI 
{
    public sealed class PageDetails
    {
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    
        [JsonPropertyName("result_count")]
        public int ResultCount { get; set; } 
    
        [JsonPropertyName("current_url")]
        public string CurrentUrl { get; set; }

        [JsonPropertyName("prev_url")] 
        public string PrevUrl { get; set; }
    
        [JsonPropertyName("next_url")]
        public string NextUrl { get; set; }
    }
}
