using System.Text.Json.Serialization;

namespace AssemblyAI;

public class FinalTranscript : RealtimeBaseTranscript
{
    [JsonPropertyName("message_type")]
    public string MessageType { get; } = "FinalTranscript";

    [JsonPropertyName("punctuated")]
    public bool Punctuated { get; init; }

    [JsonPropertyName("text_formatted")]
    public string TextFormatted { get; init; } = null!;
}