using System.Text.Json.Serialization;
using OneOf;

namespace AssemblyaiApi;

public class LemurQuestion
{
    /// <summary>
    /// The question you wish to ask. For more complex questions use default model.
    /// </summary>
    [JsonPropertyName("question")]
    public string Question { get; init; }

    /// <summary>
    /// Any context about the transcripts you wish to provide. This can be a string or any object.
    /// </summary>
    [JsonPropertyName("context")]
    public OneOf<string, Dictionary<string, object>>? Context { get; init; }

    /// <summary>
    /// How you want the answer to be returned. This can be any text. Can't be used with answer_options. Examples: "short sentence", "bullet points"
    /// </summary>
    [JsonPropertyName("answer_format")]
    public string? AnswerFormat { get; init; }

    /// <summary>
    /// What discrete options to return. Useful for precise responses. Can't be used with answer_format. Example: ["Yes", "No"]
    /// </summary>
    [JsonPropertyName("answer_options")]
    public List<string>? AnswerOptions { get; init; }
}
