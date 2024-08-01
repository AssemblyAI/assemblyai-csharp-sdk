using System.Text.Json.Serialization;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

public record TranscriptListItem
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("resource_url")]
    public required string ResourceUrl { get; set; }

    [JsonPropertyName("status")]
    public required TranscriptStatus Status { get; set; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; set; }

    [JsonPropertyName("completed")]
    public DateTime? Completed { get; set; }

    [JsonPropertyName("audio_url")]
    public required string AudioUrl { get; set; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
