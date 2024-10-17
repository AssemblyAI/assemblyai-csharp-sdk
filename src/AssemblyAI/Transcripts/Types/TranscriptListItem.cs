using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptListItem
{
    /// <summary>
    /// The unique identifier for the transcript
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The URL to retrieve the transcript
    /// </summary>
    [JsonPropertyName("resource_url")]
    public required string ResourceUrl { get; set; }

    /// <summary>
    /// The status of the transcript
    /// </summary>
    [JsonPropertyName("status")]
    public required TranscriptStatus Status { get; set; }

    /// <summary>
    /// The date and time the transcript was created
    /// </summary>
    [JsonPropertyName("created")]
    public required DateTime Created { get; set; }

    /// <summary>
    /// The date and time the transcript was completed
    /// </summary>
    [JsonPropertyName("completed")]
    public DateTime? Completed { get; set; }

    /// <summary>
    /// The URL to the audio file
    /// </summary>
    [JsonPropertyName("audio_url")]
    public required string AudioUrl { get; set; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
