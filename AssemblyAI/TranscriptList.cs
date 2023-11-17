using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptList
    {
        [JsonPropertyName("page_details")] 
        public PageDetails PageDetails { get; init; } = null!;

        [JsonPropertyName("transcripts")] 
        public List<TranscriptListItem> Transcripts { get; init; } = null!;
    }
}
