using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public sealed class TranscriptIEnumerable
    {
        [JsonPropertyName("page_details")] 
        public PageDetails PageDetails { get; set; }

        [JsonPropertyName("transcripts")] 
        public IEnumerable<TranscriptListItem> Transcripts { get; set; }
    }
}
