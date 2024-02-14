using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class WordSearchResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("matches")]
        public IEnumerable<WordSearchMatch> Matches { get; set; }
    }
}