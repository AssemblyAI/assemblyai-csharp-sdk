using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class SentimentAnalysisResult
{
    /// <summary>
    /// The transcript of the sentence
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// The starting time, in milliseconds, of the sentence
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; init; }

    /// <summary>
    /// The ending time, in milliseconds, of the sentence
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; init; }

    /// <summary>
    /// The detected sentiment for the sentence, one of POSITIVE, NEUTRAL, NEGATIVE
    /// </summary>
    [JsonPropertyName("sentiment")]
    public Sentiment Sentiment { get; init; }

    /// <summary>
    /// The confidence score for the detected sentiment of the sentence, from 0 to 1
    /// </summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    /// <summary>
    /// The speaker of the sentence if [Speaker Diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, else null
    /// </summary>
    [JsonPropertyName("speaker")]
    public string? Speaker { get; init; }
}
