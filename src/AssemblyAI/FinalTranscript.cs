using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class FinalTranscript : RealtimeBaseTranscript
    {
        [JsonPropertyName("message_type")]
        public string MessageType { get; } = "FinalTranscript";

        [JsonPropertyName("punctuated")]
        public bool Punctuated { get; set; }
    
        [JsonPropertyName("text_formatted")]
        public bool TextFormatted { get; set; }
    }
}