using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record CreateRealtimeTemporaryTokenParams
{
    /// <summary>
    /// The amount of time until the token expires in seconds
    /// </summary>
    [JsonPropertyName("expires_in")]
    public required int ExpiresIn { get; init; }
}
