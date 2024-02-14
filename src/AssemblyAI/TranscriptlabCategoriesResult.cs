using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TranscriptlabCategoriesResult
    {
        [JsonPropertyName("status")]
        public AudioIntelligenceModelStatus Status { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<TopicDetectionResult> Results { get; set; }

        [JsonPropertyName("summary")]
        public IReadOnlyDictionary<string, double> Summary { get; set; }
    }
}