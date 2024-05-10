using System.Text.Json.Serialization;
using AssemblyaiApi;
using OneOf;

namespace AssemblyaiApi;

public class LemurBaseParams
{
    /// <summary>
    /// A list of completed transcripts with text. Up to a maximum of 100 files or 100 hours, whichever is lower.
    /// Use either transcript_ids or input_text as input into LeMUR.
    /// </summary>
    [JsonPropertyName("transcript_ids")]
    public List<string>? TranscriptIDs { get; init; }

    /// <summary>
    /// Custom formatted transcript data. Maximum size is the context limit of the selected model, which defaults to 100000.
    /// Use either transcript_ids or input_text as input into LeMUR.
    /// </summary>
    [JsonPropertyName("input_text")]
    public string? InputText { get; init; }

    /// <summary>
    /// Context to provide the model. This can be a string or a free-form JSON value.
    /// </summary>
    [JsonPropertyName("context")]
    public OneOf<string, Dictionary<string, object>>? Context { get; init; }

    /// <summary>
    /// The model that is used for the final prompt after compression is performed.
    /// Defaults to "default".
    /// </summary>
    [JsonPropertyName("final_model")]
    public LemurModel? FinalModel { get; init; }

    /// <summary>
    /// Max output size in tokens, up to 4000
    /// </summary>
    [JsonPropertyName("max_output_size")]
    public int? MaxOutputSize { get; init; }

    /// <summary>
    /// The temperature to use for the model.
    /// Higher values result in answers that are more creative, lower values are more conservative.
    /// Can be any value between 0.0 and 1.0 inclusive.
    /// </summary>
    [JsonPropertyName("temperature")]
    public double? Temperature { get; init; }
}
