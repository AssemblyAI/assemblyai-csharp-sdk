using System.Text.Json.Serialization;

namespace AssemblyAI.Transcripts;

public sealed class CreateTranscriptParameters : CreateTranscriptOptionalParameters
{
    [JsonPropertyName("audio_url")] 
    public string AudioUrl { get; init; } = null!;
}  