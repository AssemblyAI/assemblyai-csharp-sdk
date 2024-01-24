using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class CreateRealtimeTemporaryTokenParameters
    {
        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; init; }
    }   
}