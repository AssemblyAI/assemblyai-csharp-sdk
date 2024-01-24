using System.Text.Json.Serialization;

namespace AssemblyAI;

public class LemurBaseParameters
{
    [JsonPropertyName("transcript_ids")]
    public List<string> TranscriptIds { get; init; } = new List<string>();

    [JsonPropertyName("context")]
    public LemurBaseParametersContext? Context { get; init; } = null;

    [JsonPropertyName("final_model")]
    public LemurModel? FinalModel { get; init; } = null;

    [JsonPropertyName("max_output_size")]
    public int? MaxOutputSize { get; init; } = null;

    [JsonPropertyName("temperature")]
    public double? Temperature { get; init; } = null;
}