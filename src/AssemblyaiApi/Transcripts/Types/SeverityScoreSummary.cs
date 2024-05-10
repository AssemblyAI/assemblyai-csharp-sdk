using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class SeverityScoreSummary
{
    [JsonPropertyName("low")]
    public double Low { get; init; }

    [JsonPropertyName("medium")]
    public double Medium { get; init; }

    [JsonPropertyName("high")]
    public double High { get; init; }
}
