using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class Chapter
    {
        [JsonPropertyName("gist")]
        public string Gist { get; set; }
    
        [JsonPropertyName("headline")]
        public string Headline { get; set; }
    
        [JsonPropertyName("summary")]
        public string Summary { get; set; }
    
        [JsonPropertyName("start")] 
        public int Start { get; set; }
    
        [JsonPropertyName("end")] 
        public int End { get; set; }
    }
}