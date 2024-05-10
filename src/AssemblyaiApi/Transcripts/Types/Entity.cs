using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class Entity
{
    /// <summary>
    /// The type of entity for the detected entity
    /// </summary>
    [JsonPropertyName("entity_type")]
    public EntityType EntityType { get; init; }

    /// <summary>
    /// The text for the detected entity
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// The starting time, in milliseconds, at which the detected entity appears in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; init; }

    /// <summary>
    /// The ending time, in milliseconds, for the detected entity in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; init; }
}
