using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class LemurTaskParams
{
    /// <summary>
    /// Your text to prompt the model to produce a desired output, including any context you want to pass into the model.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; }
}
