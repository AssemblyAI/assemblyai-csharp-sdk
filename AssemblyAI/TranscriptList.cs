using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptIEnumerable
    {
        [JsonPropertyName("page_details")] 
        public PageDetails PageDetails { get; init; } = null!;

        [JsonPropertyName("transcripts")] 
        public IEnumerable<TranscriptIEnumerableItem> Transcripts { get; init; } = null!;
    }
}
