using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public record SessionBegins
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; init; }

    /// <summary>
    /// Unique identifier for the established session
    /// </summary>
    [JsonPropertyName("session_id")]
    public required string SessionId { get; init; }

    /// <summary>
    /// Timestamp when this session will expire
    /// </summary>
    [JsonPropertyName("expires_at")]
    public required DateTime ExpiresAt { get; init; }
}
