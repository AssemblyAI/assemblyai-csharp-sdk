using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class TranscriptCustomSpelling
{
    /// <summary>
    /// Words or phrases to replace
    /// </summary>
    [JsonPropertyName("from")]
    public List<string> From { get; init; }

    /// <summary>
    /// Word or phrase to replace with
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; init; }
}
