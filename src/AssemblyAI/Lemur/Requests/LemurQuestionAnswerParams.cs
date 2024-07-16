using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record LemurQuestionAnswerParams
{
    /// <summary>
    /// A list of questions to ask
    /// </summary>
    [JsonPropertyName("questions")]
    public IEnumerable<LemurQuestion> Questions { get; init; } = new List<LemurQuestion>();
}
