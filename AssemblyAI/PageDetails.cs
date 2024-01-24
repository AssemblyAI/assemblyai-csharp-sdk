using System.Text.Json.Serialization;

namespace AssemblyAI 
{
    public sealed class PageDetails
    {
        [JsonPropertyName("limit")]
        public int Limit { get; init; }
    
        [JsonPropertyName("result_count")]
        public int ResultCount { get; init; } 
    
        [JsonPropertyName("current_url")]
        public string CurrentUrl { get; init; } = null!;

        [JsonPropertyName("prev_url")] 
        public string PrevUrl { get; init; } = null!;
    
        [JsonPropertyName("next_url")]
        public string? NextUrl { get; init; }
    }
}
