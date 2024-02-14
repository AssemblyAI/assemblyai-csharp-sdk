using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptWordSearchRequest
    {
        [JsonPropertyName("words")] 
        public string? Words { get; init; }
    }   
}