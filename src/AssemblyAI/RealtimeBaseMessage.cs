using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class RealtimeBaseMessage
    {
        [JsonPropertyName("messageType")]
        public MessageType MessageType { get; set; }
    }
}