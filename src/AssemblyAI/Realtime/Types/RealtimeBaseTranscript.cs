using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class RealtimeBaseTranscript
{
    /// <summary>
    /// Start time of audio sample relative to session start, in milliseconds
    /// </summary>
    [JsonPropertyName("audio_start")]
    public int AudioStart { get; init; }

    /// <summary>
    /// End time of audio sample relative to session start, in milliseconds
    /// </summary>
    [JsonPropertyName("audio_end")]
    public int AudioEnd { get; init; }

    /// <summary>
    /// The confidence score of the entire transcription, between 0 and 1
    /// </summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; init; }

    /// <summary>
    /// The partial transcript for your audio
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// An array of objects, with the information for each word in the transcription text.
    /// Includes the start and end time of the word in milliseconds, the confidence score of the word, and the text, which is the word itself.
    /// </summary>
    [JsonPropertyName("words")]
    public List<Word> Words { get; init; }

    /// <summary>
    /// The timestamp for the partial transcript
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime Created { get; init; }
}
