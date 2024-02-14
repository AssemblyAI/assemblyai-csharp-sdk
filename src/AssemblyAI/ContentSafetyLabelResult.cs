using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class ContentSafetyLabelResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    
        [JsonPropertyName("labels")]
        public IEnumerable<ContentSafetyLabel> labels { get; set; }
    
        [JsonPropertyName("sentences_idx_start")]
        public int SentencesIdxStart { get; set; }
    
        [JsonPropertyName("sentences_idx_end")]
        public int SentencesIdxEnd { get; set; }

        [JsonPropertyName("timestamp")]
        public Timestamp Timestamp { get; set; }
    }
}