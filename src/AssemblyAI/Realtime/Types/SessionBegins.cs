using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

public record SessionBegins
{
    /// <summary>
    /// Describes the type of the message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; set; }

    /// <summary>
    /// Unique identifier for the established session
    /// </summary>
    [JsonPropertyName("session_id")]
    public required string SessionId { get; set; }

    /// <summary>
    /// Timestamp when this session will expire
    /// </summary>
    [JsonPropertyName("expires_at")]
    public required DateTime ExpiresAt { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
