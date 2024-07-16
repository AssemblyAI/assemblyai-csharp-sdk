using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TranscriptReadyNotification
{
    /// <summary>
    /// The ID of the transcript
    /// </summary>
    [JsonPropertyName("transcript_id")]
    public required string TranscriptId { get; init; }

    /// <summary>
    /// The status of the transcript. Either completed or error.
    /// </summary>
    [JsonPropertyName("status")]
    public required TranscriptReadyStatus Status { get; init; }
}
