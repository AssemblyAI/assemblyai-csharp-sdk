using System.Text.Json.Serialization;
using AssemblyAI.Realtime;

#nullable enable

namespace AssemblyAI.Realtime;

public record FinalTranscript
{
    /// <summary>
    /// Describes the type of message
    /// </summary>
    [JsonPropertyName("message_type")]
    public required string MessageType { get; set; }

    /// <summary>
    /// Whether the text is punctuated and cased
    /// </summary>
    [JsonPropertyName("punctuated")]
    public required bool Punctuated { get; set; }

    /// <summary>
    /// Whether the text is formatted, for example Dollar -> $
    /// </summary>
    [JsonPropertyName("text_formatted")]
    public required bool TextFormatted { get; set; }

    /// <summary>
    /// Start time of audio sample relative to session start, in milliseconds
    /// </summary>
    [JsonPropertyName("audio_start")]
    public required int AudioStart { get; set; }

    /// <summary>
    /// End time of audio sample relative to session start, in milliseconds
    /// </summary>
    [JsonPropertyName("audio_end")]
    public required int AudioEnd { get; set; }

    /// <summary>
    /// The confidence score of the entire transcription, between 0 and 1
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    /// <summary>
    /// The partial transcript for your audio
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// An array of objects, with the information for each word in the transcription text.
    /// Includes the start and end time of the word in milliseconds, the confidence score of the word, and the text, which is the word itself.
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<Word> Words { get; set; } = new List<Word>();

    /// <summary>
    /// The timestamp for the partial transcript
    /// </summary>
    [JsonPropertyName("created")]
    public required DateTime Created { get; set; }
}
