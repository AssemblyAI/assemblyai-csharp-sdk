using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record Timestamp
{
    /// <summary>
    /// The start time in milliseconds
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; init; }

    /// <summary>
    /// The end time in milliseconds
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; init; }
}
