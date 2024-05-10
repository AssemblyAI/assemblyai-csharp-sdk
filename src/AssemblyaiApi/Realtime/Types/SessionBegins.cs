using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class SessionBegins
{
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; }

    /// <summary>
    /// Unique identifier for the established session
    /// </summary>
    [JsonPropertyName("session_id")]
    public string SessionID { get; init; }

    /// <summary>
    /// Timestamp when this session will expire
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; init; }
}
