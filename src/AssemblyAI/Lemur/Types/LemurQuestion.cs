using System.Text.Json.Serialization;
using AssemblyAI.Core;
using OneOf;

#nullable enable

namespace AssemblyAI.Lemur;

public record LemurQuestion
{
    /// <summary>
    /// The question you wish to ask. For more complex questions use default model.
    /// </summary>
    [JsonPropertyName("question")]
    public required string Question { get; set; }

    /// <summary>
    /// Any context about the transcripts you wish to provide. This can be a string or any object.
    /// </summary>
    [JsonPropertyName("context")]
    [JsonConverter(typeof(OneOfSerializer<OneOf<string, object>>))]
    public OneOf<string, object>? Context { get; set; }

    /// <summary>
    /// How you want the answer to be returned. This can be any text. Can't be used with answer_options. Examples: "short sentence", "bullet points"
    /// </summary>
    [JsonPropertyName("answer_format")]
    public string? AnswerFormat { get; set; }

    /// <summary>
    /// What discrete options to return. Useful for precise responses. Can't be used with answer_format. Example: ["Yes", "No"]
    /// </summary>
    [JsonPropertyName("answer_options")]
    public IEnumerable<string>? AnswerOptions { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
