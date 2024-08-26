using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record ContentSafetyLabel
{
    /// <summary>
    /// The label of the sensitive topic
    /// </summary>
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    /// <summary>
    /// The confidence score for the topic being discussed, from 0 to 1
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// How severely the topic is discussed in the section, from 0 to 1
    /// </summary>
    [JsonPropertyName("severity")]
    public required double Severity { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
