using System.Text.Json.Serialization;

namespace AssemblyAI;

public class AudioData
{
    [JsonPropertyName("audio_data")] 
    public string AudioData_ { get; init; } = null!;
}