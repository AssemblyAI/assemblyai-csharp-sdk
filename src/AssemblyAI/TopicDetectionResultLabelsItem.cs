using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TopicDetectionResultLabelsItem
    {
        [JsonPropertyName("relevance")]
        public double Relevance { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}