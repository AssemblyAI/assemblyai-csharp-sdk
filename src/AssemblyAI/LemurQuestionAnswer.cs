using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurQuestionAnswer
{
    [JsonPropertyName("question")]
    public string Question { get; init; } = null!;

    [JsonPropertyName("answer")]
    public string Answer { get; init; } = null!;
}