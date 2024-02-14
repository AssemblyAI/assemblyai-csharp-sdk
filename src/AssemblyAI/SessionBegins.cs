using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class SessionBegins
    {
        [JsonPropertyName("message_type")]
        public string MessageType = "SessionBegins";
    
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }

        [JsonPropertyName("expiresAt")]
        public string ExpiresAt { get; set; }
    }
}