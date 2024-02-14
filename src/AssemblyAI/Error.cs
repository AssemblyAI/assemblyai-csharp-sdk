using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class ErrorParams
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
    
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}