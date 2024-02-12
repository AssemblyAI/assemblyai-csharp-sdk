using System.Text.Json.Serialization;

namespace AssemblyAI;

public class PartialTranscript
{
    [JsonPropertyName("audio_start")]
    public int AudioStart { get; init; }

    [JsonPropertyName("audio_end")]
    public int AudioEnd { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("words")]
    public IEnumerable<Word> Words { get; init; } = null!;

    [JsonPropertyName("created")]
    public string Created { get; init; } = null!;
}