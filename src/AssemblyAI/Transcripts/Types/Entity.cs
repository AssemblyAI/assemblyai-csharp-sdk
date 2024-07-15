using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record Entity
{
    /// <summary>
    /// The type of entity for the detected entity
    /// </summary>
    [JsonPropertyName("entity_type")]
    public required EntityType EntityType { get; init; }

    /// <summary>
    /// The text for the detected entity
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>
    /// The starting time, in milliseconds, at which the detected entity appears in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; init; }

    /// <summary>
    /// The ending time, in milliseconds, for the detected entity in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; init; }
}
