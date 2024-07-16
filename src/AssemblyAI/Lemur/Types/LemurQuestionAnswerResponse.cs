using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record LemurQuestionAnswerResponse
{
    /// <summary>
    /// The answers generated by LeMUR and their questions
    /// </summary>
    [JsonPropertyName("response")]
    public IEnumerable<LemurQuestionAnswer> Response { get; init; } =
        new List<LemurQuestionAnswer>();

    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public required string RequestId { get; init; }

    /// <summary>
    /// The usage numbers for the LeMUR request
    /// </summary>
    [JsonPropertyName("usage")]
    public required LemurUsage Usage { get; init; }
}