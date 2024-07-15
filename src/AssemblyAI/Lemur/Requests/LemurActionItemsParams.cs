using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record LemurActionItemsParams
{
    /// <summary>
    /// How you want the action items to be returned. This can be any text.
    /// Defaults to "Bullet Points".
    ///
    /// </summary>
    [JsonPropertyName("answer_format")]
    public string? AnswerFormat { get; init; }
}
