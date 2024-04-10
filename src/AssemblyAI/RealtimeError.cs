using System.Text.Json.Serialization;

namespace AssemblyAI;

public class RealtimeError
{
    [JsonPropertyName("error")]
    public string Text { get; init; } = null!;

    public override string ToString() => Text;
}