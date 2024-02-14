using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TerminateSessionParams : RealtimeBaseMessage
    {
        [JsonPropertyName("terminateSession")]
        public bool TerminateSession { get; set; }
    }
}