using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurQuestionAnswer
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("answer")]
        public string Answer { get; set; }
    }
}