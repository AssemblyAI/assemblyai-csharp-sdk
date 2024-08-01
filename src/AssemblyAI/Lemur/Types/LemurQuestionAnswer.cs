using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Lemur;

public record LemurQuestionAnswer
{
    /// <summary>
    /// The question for LeMUR to answer
    /// </summary>
    [JsonPropertyName("question")]
    public required string Question { get; set; }

    /// <summary>
    /// The answer generated by LeMUR
    /// </summary>
    [JsonPropertyName("answer")]
    public required string Answer { get; set; }
}
