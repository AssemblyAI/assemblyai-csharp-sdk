using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptGetSubtitlesRequest
    {
        [JsonPropertyName("charsPerCaption")]
        public int? CharsPerCaption { get; init; }
    }   
}