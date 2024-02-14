using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class TranscriptCustomSpelling
    {
        [JsonPropertyName("from")]
        public IEnumerable<string> From { get; set; }
    
        [JsonPropertyName("to")]
        public string To { get; set; }
    }
}