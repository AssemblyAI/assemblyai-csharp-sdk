using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class Entity
    {
        [JsonPropertyName("entity_type")]
        private EntityType EntityType { get; set; }

        [JsonPropertyName("text")]
        private string Text { get; set; }

        [JsonPropertyName("start")]
        private int Start { get; set; }

        [JsonPropertyName("end")]
        private int End  { get; set; }
    }
}