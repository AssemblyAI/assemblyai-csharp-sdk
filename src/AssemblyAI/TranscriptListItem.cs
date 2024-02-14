using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptListItem
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }
    
        [JsonPropertyName("resource_url")] 
        public string ResourceUrl { get; set; }
    
        [JsonPropertyName("status")] 
        public string TranscriptStatus { get; set; }
        
        [JsonPropertyName("created")] 
        public string Created { get; set; }
        
        [JsonPropertyName("completed")] 
        public string Completed { get; set; }
        
        [JsonPropertyName("audio_url")] 
        public string AudioUrl { get; set; }
    }
}

