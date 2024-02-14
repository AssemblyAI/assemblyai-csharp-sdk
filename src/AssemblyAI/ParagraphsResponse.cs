using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class ParagraphsResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("audio_duration")]
        public double AudioDuration { get; set; }

        [JsonPropertyName("paragraphs")]
        public IEnumerable<TranscriptParagraph> Paragraphs { get; set; }
    }
}

