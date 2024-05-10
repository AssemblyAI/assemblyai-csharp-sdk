using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class LemurQuestionAnswerResponse
{
    /// <summary>
    /// The answers generated by LeMUR and their questions
    /// </summary>
    [JsonPropertyName("response")]
    public List<LemurQuestionAnswer> Response { get; init; }

    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestID { get; init; }
}
