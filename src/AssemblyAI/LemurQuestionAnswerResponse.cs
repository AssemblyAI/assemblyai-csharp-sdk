using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class LemurQuestionAnswerResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    
        [JsonPropertyName("response")]
        public IEnumerable<LemurQuestionAnswer> Response { get; set; }
    }
}