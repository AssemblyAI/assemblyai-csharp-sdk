using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class TranscriptOptionalParams
{
    [JsonPropertyName("language_code")]
    public TranscriptLanguageCode? LanguageCode { get; init; }

    /// <summary>
    /// Enable Automatic Punctuation, can be true or false
    /// </summary>
    [JsonPropertyName("punctuate")]
    public bool? Punctuate { get; init; }

    /// <summary>
    /// Enable Text Formatting, can be true or false
    /// </summary>
    [JsonPropertyName("format_text")]
    public bool? FormatText { get; init; }

    /// <summary>
    /// Enable [Dual Channel](https://www.assemblyai.com/docs/models/speech-recognition#dual-channel-transcription) transcription, can be true or false.
    /// </summary>
    [JsonPropertyName("dual_channel")]
    public bool? DualChannel { get; init; }

    [JsonPropertyName("speech_model")]
    public SpeechModel? SpeechModel { get; init; }

    /// <summary>
    /// The URL to which AssemblyAI send webhooks upon transcription completion
    /// </summary>
    [JsonPropertyName("webhook_url")]
    public string? WebhookUrl { get; init; }

    /// <summary>
    /// The header name which should be sent back with webhook calls
    /// </summary>
    [JsonPropertyName("webhook_auth_header_name")]
    public string? WebhookAuthHeaderName { get; init; }

    /// <summary>
    /// Specify a header name and value to send back with a webhook call for added security
    /// </summary>
    [JsonPropertyName("webhook_auth_header_value")]
    public string? WebhookAuthHeaderValue { get; init; }

    /// <summary>
    /// Enable Key Phrases, either true or false
    /// </summary>
    [JsonPropertyName("auto_highlights")]
    public bool? AutoHighlights { get; init; }

    /// <summary>
    /// The point in time, in milliseconds, to begin transcribing in your media file
    /// </summary>
    [JsonPropertyName("audio_start_from")]
    public int? AudioStartFrom { get; init; }

    /// <summary>
    /// The point in time, in milliseconds, to stop transcribing in your media file
    /// </summary>
    [JsonPropertyName("audio_end_at")]
    public int? AudioEndAt { get; init; }

    /// <summary>
    /// The list of custom vocabulary to boost transcription probability for
    /// </summary>
    [JsonPropertyName("word_boost")]
    public List<string>? WordBoost { get; init; }

    /// <summary>
    /// The word boost parameter value
    /// </summary>
    [JsonPropertyName("boost_param")]
    public TranscriptBoostParam? BoostParam { get; init; }

    /// <summary>
    /// Filter profanity from the transcribed text, can be true or false
    /// </summary>
    [JsonPropertyName("filter_profanity")]
    public bool? FilterProfanity { get; init; }

    /// <summary>
    /// Redact PII from the transcribed text using the Redact PII model, can be true or false
    /// </summary>
    [JsonPropertyName("redact_pii")]
    public bool? RedactPii { get; init; }

    /// <summary>
    /// Generate a copy of the original media file with spoken PII "beeped" out, can be true or false. See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more details.
    /// </summary>
    [JsonPropertyName("redact_pii_audio")]
    public bool? RedactPiiAudio { get; init; }

    /// <summary>
    /// Controls the filetype of the audio created by redact_pii_audio. Currently supports mp3 (default) and wav. See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more details.
    /// </summary>
    [JsonPropertyName("redact_pii_audio_quality")]
    public RedactPiiAudioQuality? RedactPiiAudioQuality { get; init; }

    /// <summary>
    /// The list of PII Redaction policies to enable. See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more details.
    /// </summary>
    [JsonPropertyName("redact_pii_policies")]
    public List<PiiPolicy>? RedactPiiPolicies { get; init; }

    [JsonPropertyName("redact_pii_sub")]
    public SubstitutionPolicy? RedactPiiSub { get; init; }

    /// <summary>
    /// Enable [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization), can be true or false
    /// </summary>
    [JsonPropertyName("speaker_labels")]
    public bool? SpeakerLabels { get; init; }

    /// <summary>
    /// Tells the speaker label model how many speakers it should attempt to identify, up to 10. See [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) for more details.
    /// </summary>
    [JsonPropertyName("speakers_expected")]
    public int? SpeakersExpected { get; init; }

    /// <summary>
    /// Enable [Content Moderation](https://www.assemblyai.com/docs/models/content-moderation), can be true or false
    /// </summary>
    [JsonPropertyName("content_safety")]
    public bool? ContentSafety { get; init; }

    /// <summary>
    /// The confidence threshold for the Content Moderation model. Values must be between 25 and 100.
    /// </summary>
    [JsonPropertyName("content_safety_confidence")]
    public int? ContentSafetyConfidence { get; init; }

    /// <summary>
    /// Enable [Topic Detection](https://www.assemblyai.com/docs/models/topic-detection), can be true or false
    /// </summary>
    [JsonPropertyName("iab_categories")]
    public bool? IabCategories { get; init; }

    /// <summary>
    /// Enable [Automatic language detection](https://www.assemblyai.com/docs/models/speech-recognition#automatic-language-detection), either true or false.
    /// </summary>
    [JsonPropertyName("language_detection")]
    public bool? LanguageDetection { get; init; }

    /// <summary>
    /// Customize how words are spelled and formatted using to and from values
    /// </summary>
    [JsonPropertyName("custom_spelling")]
    public List<TranscriptCustomSpelling>? CustomSpelling { get; init; }

    /// <summary>
    /// Transcribe Filler Words, like "umm", in your media file; can be true or false
    /// </summary>
    [JsonPropertyName("disfluencies")]
    public bool? Disfluencies { get; init; }

    /// <summary>
    /// Enable [Sentiment Analysis](https://www.assemblyai.com/docs/models/sentiment-analysis), can be true or false
    /// </summary>
    [JsonPropertyName("sentiment_analysis")]
    public bool? SentimentAnalysis { get; init; }

    /// <summary>
    /// Enable [Auto Chapters](https://www.assemblyai.com/docs/models/auto-chapters), can be true or false
    /// </summary>
    [JsonPropertyName("auto_chapters")]
    public bool? AutoChapters { get; init; }

    /// <summary>
    /// Enable [Entity Detection](https://www.assemblyai.com/docs/models/entity-detection), can be true or false
    /// </summary>
    [JsonPropertyName("entity_detection")]
    public bool? EntityDetection { get; init; }

    /// <summary>
    /// Reject audio files that contain less than this fraction of speech.
    /// Valid values are in the range [0, 1] inclusive.
    /// </summary>
    [JsonPropertyName("speech_threshold")]
    public double? SpeechThreshold { get; init; }

    /// <summary>
    /// Enable [Summarization](https://www.assemblyai.com/docs/models/summarization), can be true or false
    /// </summary>
    [JsonPropertyName("summarization")]
    public bool? Summarization { get; init; }

    /// <summary>
    /// The model to summarize the transcript
    /// </summary>
    [JsonPropertyName("summary_model")]
    public SummaryModel? SummaryModel { get; init; }

    /// <summary>
    /// The type of summary
    /// </summary>
    [JsonPropertyName("summary_type")]
    public SummaryType? SummaryType { get; init; }

    /// <summary>
    /// Enable custom topics, either true or false
    /// </summary>
    [JsonPropertyName("custom_topics")]
    public bool? CustomTopics { get; init; }

    /// <summary>
    /// The list of custom topics
    /// </summary>
    [JsonPropertyName("topics")]
    public List<string>? Topics { get; init; }
}
