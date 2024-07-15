using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TranscriptList
{
    [JsonPropertyName("page_details")]
    public required PageDetails PageDetails { get; init; }

    [JsonPropertyName("transcripts")]
    public IEnumerable<TranscriptListItem> Transcripts { get; init; } =
        new List<TranscriptListItem>();
}
