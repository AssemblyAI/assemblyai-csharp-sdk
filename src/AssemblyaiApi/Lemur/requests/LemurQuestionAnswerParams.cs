using AssemblyaiApi;

namespace AssemblyaiApi;

public class LemurQuestionAnswerParams
{
    /// <summary>
    /// A list of questions to ask
    /// </summary>
    public List<LemurQuestion> Questions { get; init; }
}
