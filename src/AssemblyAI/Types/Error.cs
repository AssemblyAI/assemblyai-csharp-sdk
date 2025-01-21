using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

public record Error
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error_ { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
