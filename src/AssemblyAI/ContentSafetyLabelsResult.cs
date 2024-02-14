using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class ContentSafetyLabelsResult
    {
    
        [JsonPropertyName("status")]
        public AudioIntelligenceModelStatus Status { get; set; }
    
        [JsonPropertyName("results")]
        public IEnumerable<ContentSafetyLabelResult> Results { get; set; }
    
        [JsonPropertyName("summary")]
        public IReadOnlyDictionary<string, double> Summary { get; set; }
    
        [JsonPropertyName("severity_score_summary")]
        public IReadOnlyDictionary<string, SeverityScoreSummary> SeverityScoreSummary { get; set; }
    }
}