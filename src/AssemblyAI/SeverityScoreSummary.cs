using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class SeverityScoreSummary
    {
        [JsonPropertyName("low")]
        public double Low { get; set; }
    
        [JsonPropertyName("medium")]
        public double Medium { get; set; }
    
        [JsonPropertyName("high")]
        public double High { get; set; }
    }
}