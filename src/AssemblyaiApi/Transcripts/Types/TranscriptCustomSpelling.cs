using System.Text.Json.Serialization;

namespace AssemblyaiApi;

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
