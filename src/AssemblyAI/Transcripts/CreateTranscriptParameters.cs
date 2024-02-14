using System.Text.Json.Serialization;

namespace AssemblyAI.Transcripts
{
    public sealed class CreateTranscriptParameters : CreateTranscriptOptionalParameters
    {
        [JsonPropertyName("audioUrl")]
        public string AudioUrl { get; set; }
    }   
}