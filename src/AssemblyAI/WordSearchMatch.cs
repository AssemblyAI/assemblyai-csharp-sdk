using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class WordSearchMatch
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("timestamps")]
        public IEnumerable<IEnumerable<int>> Timestamps { get; set; }

        [JsonPropertyName("indexes")]
        public IEnumerable<int> Indexes { get; set; }
    }
}