using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record TranscriptCustomSpelling
{
    /// <summary>
    /// Words or phrases to replace
    /// </summary>
    [JsonPropertyName("from")]
    public IEnumerable<string> From { get; init; } = new List<string>();

    /// <summary>
    /// Word or phrase to replace with
    /// </summary>
    [JsonPropertyName("to")]
    public required string To { get; init; }
}
