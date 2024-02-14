using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurTaskResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    
        [JsonPropertyName("response")]
        public string Response { get; set; }
    }
}