using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class TranscriptList
{
    [JsonPropertyName("page_details")]
    public PageDetails PageDetails { get; init; }

    [JsonPropertyName("transcripts")]
    public List<TranscriptListItem> Transcripts { get; init; }
}
