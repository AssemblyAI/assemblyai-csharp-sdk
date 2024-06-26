using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class LemurQuestionAnswer
{
    /// <summary>
    /// The question for LeMUR to answer
    /// </summary>
    [JsonPropertyName("question")]
    public string Question { get; init; }

    /// <summary>
    /// The answer generated by LeMUR
    /// </summary>
    [JsonPropertyName("answer")]
    public string Answer { get; init; }
}
