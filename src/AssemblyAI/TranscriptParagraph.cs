using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TranscriptParagraph
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("words")]
        public IEnumerable<TranscriptWord> Words { get; set; }
    }
}