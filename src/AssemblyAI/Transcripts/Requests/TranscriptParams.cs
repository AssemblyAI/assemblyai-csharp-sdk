using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class TranscriptParams
{
    /// <summary>
    /// The URL of the audio or video file to transcribe.
    /// </summary>
    [JsonPropertyName("audio_url")]
    public string AudioUrl { get; init; }
}
