using System.Text.Json.Serialization;

namespace AssemblyAI;

public class PartialTranscript
{
    [JsonPropertyName("audioStart")]
    public int AudioStart { get; init; }

    [JsonPropertyName("audioEnd")]
    public int AudioEnd { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    [JsonPropertyName("words")]
    public List<Word> Words { get; init; } = null!;

    [JsonPropertyName("created")]
    public string Created { get; init; } = null!;
}