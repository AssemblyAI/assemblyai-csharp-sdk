using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class ContentSafetyLabel
{
    /// <summary>
    /// The label of the sensitive topic
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; init; }

    /// <summary>
    /// The confidence score for the topic being discussed, from 0 to 1
    /// </summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    /// <summary>
    /// How severely the topic is discussed in the section, from 0 to 1
    /// </summary>
    [JsonPropertyName("severity")]
    public double Severity { get; init; }
}
