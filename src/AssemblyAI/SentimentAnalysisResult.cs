using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class SentimentAnalysisResult
    {
        [JsonPropertyName("text")] 
        public string Text { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("sentiment")]
        public Sentiment Sentiment { get; set; } 

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("speaker")]
        public string Speaker { get; set; }
    }
}