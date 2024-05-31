using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class Timestamp
{
    /// <summary>
    /// The start time in milliseconds
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; init; }

    /// <summary>
    /// The end time in milliseconds
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; init; }
}
