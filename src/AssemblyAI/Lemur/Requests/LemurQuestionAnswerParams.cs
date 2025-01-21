using System.Text.Json.Serialization;
using AssemblyAI.Core;
using OneOf;

#nullable enable

namespace AssemblyAI.Lemur;

public record LemurQuestionAnswerParams
{
    /// <summary>
    /// A list of questions to ask
    /// </summary>
    [JsonPropertyName("questions")]
    public IEnumerable<LemurQuestion> Questions { get; set; } = new List<LemurQuestion>();

    /// <summary>
    /// A list of completed transcripts with text. Up to a maximum of 100 files or 100 hours, whichever is lower.
    /// Use either transcript_ids or input_text as input into LeMUR.
    /// </summary>
    [JsonPropertyName("transcript_ids")]
    public IEnumerable<string>? TranscriptIds { get; set; }

    /// <summary>
    /// Custom formatted transcript data. Maximum size is the context limit of the selected model, which defaults to 100000.
    /// Use either transcript_ids or input_text as input into LeMUR.
    /// </summary>
    [JsonPropertyName("input_text")]
    public string? InputText { get; set; }

    /// <summary>
    /// Context to provide the model. This can be a string or a free-form JSON value.
    /// </summary>
    [JsonPropertyName("context")]
    public OneOf<string, object>? Context { get; set; }

    /// <summary>
    /// The model that is used for the final prompt after compression is performed.
    /// </summary>
    [JsonPropertyName("final_model")]
    public LemurModel? FinalModel { get; set; }

    /// <summary>
    /// Max output size in tokens, up to 4000
    /// </summary>
    [JsonPropertyName("max_output_size")]
    public int? MaxOutputSize { get; set; }

    /// <summary>
    /// The temperature to use for the model.
    /// Higher values result in answers that are more creative, lower values are more conservative.
    /// Can be any value between 0.0 and 1.0 inclusive.
    /// </summary>
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
