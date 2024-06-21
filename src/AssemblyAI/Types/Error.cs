using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class Error
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public string Error_ { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
