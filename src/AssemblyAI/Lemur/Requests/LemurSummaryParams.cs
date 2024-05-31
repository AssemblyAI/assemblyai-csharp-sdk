using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class LemurSummaryParams
{
    /// <summary>
    /// How you want the summary to be returned. This can be any text. Examples: "TLDR", "bullet points"
    ///
    /// </summary>
    [JsonPropertyName("answer_format")]
    public string? AnswerFormat { get; init; }
}
