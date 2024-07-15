using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record Error
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error_ { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
