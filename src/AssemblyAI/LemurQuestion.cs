using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurQuestion
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("context")]
        public LemurQuestionContext Context { get; set; }

        [JsonPropertyName("answer_format")]
        public string AnswerFormat { get; set; }

        [JsonPropertyName("answer_options")]
        public IEnumerable<string> AnswerOptions { get; set; }
    }
}

