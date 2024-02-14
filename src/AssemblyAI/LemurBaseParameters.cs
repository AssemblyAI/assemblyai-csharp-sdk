using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurBaseParameters
    {
        [JsonPropertyName("transcript_ids")]
        public IEnumerable<string> TranscriptIds { get; set; } = new List<string>();

        [JsonPropertyName("context")]
        public LemurBaseParametersContext Context { get; set; } = null;

        [JsonPropertyName("final_model")]
        public LemurModel FinalModel { get; set; } = null;

        [JsonPropertyName("max_output_size")]
        public int? MaxOutputSize { get; set; } = null;

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; } = null;
    }
}