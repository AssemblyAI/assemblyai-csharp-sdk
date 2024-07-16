using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record SeverityScoreSummary
{
    [JsonPropertyName("low")]
    public required double Low { get; init; }

    [JsonPropertyName("medium")]
    public required double Medium { get; init; }

    [JsonPropertyName("high")]
    public required double High { get; init; }
}
