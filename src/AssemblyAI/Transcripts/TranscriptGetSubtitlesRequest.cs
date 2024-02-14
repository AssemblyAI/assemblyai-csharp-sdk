using System.Text.Json.Serialization;

namespace AssemblyAI.Transcripts
{
    public sealed class TranscriptGetSubtitlesRequest
    {
        [JsonPropertyName("charsPerCaption")]
        public int? CharsPerCaption { get; set; }
    }   
}