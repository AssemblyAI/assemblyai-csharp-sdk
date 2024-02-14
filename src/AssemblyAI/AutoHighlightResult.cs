using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class AutoHighlightResult
    {
        [JsonPropertyName("count")] 
        public int Count { get; set; }
    
        [JsonPropertyName("rank")] 
        public double Rank { get; set; }
    
        [JsonPropertyName("text")] 
        public string Text { get; set; }
    
        [JsonPropertyName("timestamps")] 
        public IEnumerable<Timestamp> Timestamps { get; set; }
    }
}