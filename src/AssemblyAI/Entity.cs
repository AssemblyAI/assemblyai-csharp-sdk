using System.Text.Json.Serialization;

namespace AssemblyAI;

public class Entity
{
    [JsonPropertyName("entity_type")]
    private EntityType EntityType { get; init; } = null!;

    [JsonPropertyName("text")]
    private string Text { get; init; } = null!;

    [JsonPropertyName("start")]
    private int Start { get; init; }

    [JsonPropertyName("end")]
    private int End  { get; init; }
}