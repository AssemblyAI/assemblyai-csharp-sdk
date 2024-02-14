using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class AudioDataParams
    {
        [JsonPropertyName("audio_data")] 
        public string AudioData { get; set; }
    }
}