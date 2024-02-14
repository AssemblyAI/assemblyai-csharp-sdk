using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI.Lemur
{
    public class LemurSummaryParameters
    {
        [JsonPropertyName("transcript_ids")]
        public IEnumerable<string> TranscriptIds { get; set; }

        [JsonPropertyName("context")]
        public LemurBaseParametersContext Context { get; set; }

        [JsonPropertyName("final_model")]
        public LemurModel FinalModel { get; set; }

        [JsonPropertyName("max_output_size")]
        public int? MaxOutputSize { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("answer_format")]
        public string AnswerFormat { get; set; }
    }   
}