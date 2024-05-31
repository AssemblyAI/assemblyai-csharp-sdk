using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI;

public class SessionBegins
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }

    /// <summary>
    /// Unique identifier for the established session
    /// </summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; init; }

    /// <summary>
    /// Timestamp when this session will expire
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; init; }
}
