using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record SentencesResponse
{
    /// <summary>
    /// The unique identifier for the transcript
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The confidence score for the transcript
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// The duration of the audio file in seconds
    /// </summary>
    [JsonPropertyName("audio_duration")]
    public required double AudioDuration { get; set; }

    /// <summary>
    /// An array of sentences in the transcript
    /// </summary>
    [JsonPropertyName("sentences")]
    public IEnumerable<TranscriptSentence> Sentences { get; set; } = new List<TranscriptSentence>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
