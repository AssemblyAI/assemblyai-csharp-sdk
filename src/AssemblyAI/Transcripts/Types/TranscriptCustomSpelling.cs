using System.Text.Json.Serialization;

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
}
