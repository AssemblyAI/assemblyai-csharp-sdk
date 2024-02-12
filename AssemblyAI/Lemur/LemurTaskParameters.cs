using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurTaskParameters
    {
        [JsonPropertyName("transcript_ids")]
        public IEnumerable<string> TranscriptIds { get; init; }

        [JsonPropertyName("context")]
        public LemurBaseParametersContext? Context { get; init; }

        [JsonPropertyName("final_model")]
        public LemurModel? FinalModel { get; init; }

        [JsonPropertyName("max_output_size")]
        public int? MaxOutputSize { get; init; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; init; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; init; } = null!;
    }
}