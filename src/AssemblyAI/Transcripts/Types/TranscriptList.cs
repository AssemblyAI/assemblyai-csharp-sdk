using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptList
{
    /// <summary>
    /// Details of the transcript page
    /// </summary>
    [JsonPropertyName("page_details")]
    public required PageDetails PageDetails { get; set; }

    /// <summary>
    /// An array of transcripts
    /// </summary>
    [JsonPropertyName("transcripts")]
    public IEnumerable<TranscriptListItem> Transcripts { get; set; } =
        new List<TranscriptListItem>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
