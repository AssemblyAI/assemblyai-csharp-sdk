using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TopicDetectionResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("labels")]
        public IEnumerable<TopicDetectionResultLabelsItem> Labels { get; set; }

        [JsonPropertyName("timestamp")]
        public Timestamp Timestamp { get; set; }
    }
}