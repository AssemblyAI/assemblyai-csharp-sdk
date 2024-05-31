using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class CreateRealtimeTemporaryTokenParams
{
    /// <summary>
    /// The amount of time until the token expires in seconds
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
}
