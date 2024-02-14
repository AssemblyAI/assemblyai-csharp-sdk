using System.Text.Json.Serialization;

namespace AssemblyAI;

public class TrasncriptIEnumerableParameters
{
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("status")]
    public TranscriptStatus? Status { get; init; }

    [JsonPropertyName("createdOn")]
    public string? CreatedOn { get; init; }

    [JsonPropertyName("beforeId")]
    public string? BeforeId { get; init; }

    [JsonPropertyName("afterId")]
    public string? AfterId { get; init; }

    [JsonPropertyName("throttledOnly")]
    public bool? ThrottledOnly { get; init; }
}