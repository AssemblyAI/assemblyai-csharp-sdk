using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class Word
    {
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}

