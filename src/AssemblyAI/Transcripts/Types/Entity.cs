using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record Entity
{
    /// <summary>
    /// The type of entity for the detected entity
    /// </summary>
    [JsonPropertyName("entity_type")]
    public required EntityType EntityType { get; set; }

    /// <summary>
    /// The text for the detected entity
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, at which the detected entity appears in the audio file
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The ending time, in milliseconds, for the detected entity in the audio file
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
