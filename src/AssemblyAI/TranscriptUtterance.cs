using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TranscriptUtterance
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("words")]
        public IEnumerable<TranscriptWord> Words { get; set; }

        [JsonPropertyName("speaker")]
        public string Speaker { get; set; }
    }
}