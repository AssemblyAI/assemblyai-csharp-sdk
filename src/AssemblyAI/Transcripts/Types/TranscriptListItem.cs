using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public record TranscriptListItem
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("resource_url")]
    public required string ResourceUrl { get; init; }

    [JsonPropertyName("status")]
    public required TranscriptStatus Status { get; init; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("completed")]
    public required DateTime Completed { get; init; }

    [JsonPropertyName("audio_url")]
    public required string AudioUrl { get; init; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }
}
