using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptList
{
    [JsonPropertyName("page_details")]
    public required PageDetails PageDetails { get; set; }

    [JsonPropertyName("transcripts")]
    public IEnumerable<TranscriptListItem> Transcripts { get; set; } =
        new List<TranscriptListItem>();
}
