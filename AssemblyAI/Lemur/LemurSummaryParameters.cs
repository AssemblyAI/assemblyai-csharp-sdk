using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurSummaryParameters
    {
        [JsonPropertyName("transcriptIds")]
        public List<string> TranscriptIds { get; init; }

        [JsonPropertyName("context")]
        public LemurBaseParametersContext? Context { get; init; }

        [JsonPropertyName("finalModel")]
        public LemurModel? FinalModel { get; init; }

        [JsonPropertyName("maxOutputSize")]
        public int? MaxOutputSize { get; init; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; init; }

        [JsonPropertyName("answerFormat")]
        public string? AnswerFormat { get; init; }
    }   
}