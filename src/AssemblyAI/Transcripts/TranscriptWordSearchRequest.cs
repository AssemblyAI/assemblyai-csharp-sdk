using System.Text.Json.Serialization;

namespace AssemblyAI.Transcripts
{
    public sealed class TranscriptWordSearchRequest
    {
        [JsonPropertyName("words")] 
        public string Words { get; set; }
    }   
}