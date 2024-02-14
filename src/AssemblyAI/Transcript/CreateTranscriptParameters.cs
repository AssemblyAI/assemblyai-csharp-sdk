using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class CreateTranscriptParameters : CreateTranscriptOptionalParameters
    {
        [JsonPropertyName("audioUrl")]
        public string AudioUrl { get; init; }
    }   
}