using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class CreateRealtimeTemporaryTokenParameters
    {
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }
    }   
}