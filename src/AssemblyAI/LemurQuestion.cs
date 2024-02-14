using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurQuestion
{
    [JsonPropertyName("question")]
    public string Question { get; init; } = null!;

    [JsonPropertyName("context")]
    public LemurQuestionContext? Context { get; init; }

    [JsonPropertyName("answer_format")]
    public string? AnswerFormat { get; init; }

    [JsonPropertyName("answer_options")]
    public IEnumerable<string>? AnswerOptions { get; init; }
}

