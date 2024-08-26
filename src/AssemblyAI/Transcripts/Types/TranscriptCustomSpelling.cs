using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptCustomSpelling
{
    /// <summary>
    /// Words or phrases to replace
    /// </summary>
    [JsonPropertyName("from")]
    public IEnumerable<string> From { get; set; } = new List<string>();

    /// <summary>
    /// Word or phrase to replace with
    /// </summary>
    [JsonPropertyName("to")]
    public required string To { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
