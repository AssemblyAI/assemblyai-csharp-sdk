using System.Text.Json.Serialization;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class TranscriptListItem
{
    [JsonPropertyName("id")]
    public string ID { get; init; }

    [JsonPropertyName("resource_url")]
    public string ResourceURL { get; init; }

    [JsonPropertyName("status")]
    public TranscriptStatus Status { get; init; }

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    [JsonPropertyName("completed")]
    public DateTime Completed { get; init; }

    [JsonPropertyName("audio_url")]
    public string AudioURL { get; init; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }
}
