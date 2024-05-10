using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class TranscriptReadyNotification
{
    /// <summary>
    /// The ID of the transcript
    /// </summary>
    [JsonPropertyName("transcript_id")]
    public string TranscriptID { get; init; }

    /// <summary>
    /// The status of the transcript. Either completed or error.
    /// </summary>
    [JsonPropertyName("status")]
    public TranscriptReadyStatus Status { get; init; }
}
