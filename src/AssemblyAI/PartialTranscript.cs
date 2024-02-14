using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class PartialTranscript
    {
        [JsonPropertyName("audio_start")]
        public int AudioStart { get; set; }

        [JsonPropertyName("audio_end")]
        public int AudioEnd { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("words")]
        public IEnumerable<Word> Words { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }
    }
}