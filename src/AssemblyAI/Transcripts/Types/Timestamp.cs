using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record Timestamp
{
    /// <summary>
    /// The start time in milliseconds
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The end time in milliseconds
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }
}
