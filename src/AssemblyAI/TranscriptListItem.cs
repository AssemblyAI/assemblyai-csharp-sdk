using System.Text.Json.Serialization;

namespace AssemblyAI;

public sealed class TranscriptListItem
{
    [JsonPropertyName("id")] 
    public string Id { get; init; } = null!;

    [JsonPropertyName("resource_url")] 
    public string ResourceUrl { get; init; } = null!;

    [JsonPropertyName("status")] 
    public string TranscriptStatus { get; init; } = null!;

    [JsonPropertyName("created")] 
    public string Created { get; init; } = null!;

    [JsonPropertyName("completed")] 
    public string? Completed { get; init; }

    [JsonPropertyName("audio_url")] 
    public string? AudioUrl { get; init; }
}