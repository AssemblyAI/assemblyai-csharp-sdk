using System.Text.Json.Serialization;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class Transcript
{
    /// <summary>
    /// The unique identifier of your transcript
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The language model that was used for the transcript
    /// </summary>
    [JsonPropertyName("language_model")]
    public string LanguageModel { get; init; }

    /// <summary>
    /// The acoustic model that was used for the transcript
    /// </summary>
    [JsonPropertyName("acoustic_model")]
    public string AcousticModel { get; init; }

    /// <summary>
    /// The status of your transcript. Possible values are queued, processing, completed, or error.
    /// </summary>
    [JsonPropertyName("status")]
    public TranscriptStatus Status { get; init; }

    /// <summary>
    /// The language of your audio file.
    /// Possible values are found in [Supported Languages](https://www.assemblyai.com/docs/concepts/supported-languages).
    /// The default value is 'en_us'.
    /// </summary>
    [JsonPropertyName("language_code")]
    public TranscriptLanguageCode? LanguageCode { get; init; }

    /// <summary>
    /// The URL of the media that was transcribed
    /// </summary>
    [JsonPropertyName("audio_url")]
    public string AudioUrl { get; init; }

    /// <summary>
    /// The textual transcript of your media file
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; init; }

    /// <summary>
    /// An array of temporally-sequential word objects, one for each word in the transcript.
    /// See [Speech recognition](https://www.assemblyai.com/docs/models/speech-recognition) for more information.
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord>? Words { get; init; }

    /// <summary>
    /// When dual_channel or speaker_labels is enabled, a list of turn-by-turn utterance objects.
    /// See [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) for more information.
    /// </summary>
    [JsonPropertyName("utterances")]
    public IEnumerable<TranscriptUtterance>? Utterances { get; init; }

    /// <summary>
    /// The confidence score for the transcript, between 0.0 (low confidence) and 1.0 (high confidence)
    /// </summary>
    [JsonPropertyName("confidence")]
    public double? Confidence { get; init; }

    /// <summary>
    /// The duration of this transcript object's media file, in seconds
    /// </summary>
    [JsonPropertyName("audio_duration")]
    public double? AudioDuration { get; init; }

    /// <summary>
    /// Whether Automatic Punctuation is enabled, either true or false
    /// </summary>
    [JsonPropertyName("punctuate")]
    public bool? Punctuate { get; init; }

    /// <summary>
    /// Whether Text Formatting is enabled, either true or false
    /// </summary>
    [JsonPropertyName("format_text")]
    public bool? FormatText { get; init; }

    /// <summary>
    /// Whether [Dual channel transcription](https://www.assemblyai.com/docs/models/speech-recognition#dual-channel-transcription) was enabled in the transcription request, either true or false
    /// </summary>
    [JsonPropertyName("dual_channel")]
    public bool? DualChannel { get; init; }

    [JsonPropertyName("speech_model")]
    public SpeechModel? SpeechModel { get; init; }

    /// <summary>
    /// The URL to which we send webhooks upon transcription completion
    /// </summary>
    [JsonPropertyName("webhook_url")]
    public string? WebhookUrl { get; init; }

    /// <summary>
    /// The status code we received from your server when delivering your webhook, if a webhook URL was provided
    /// </summary>
    [JsonPropertyName("webhook_status_code")]
    public int? WebhookStatusCode { get; init; }

    /// <summary>
    /// Whether webhook authentication details were provided
    /// </summary>
    [JsonPropertyName("webhook_auth")]
    public bool WebhookAuth { get; init; }

    /// <summary>
    /// The header name which should be sent back with webhook calls
    /// </summary>
    [JsonPropertyName("webhook_auth_header_name")]
    public string? WebhookAuthHeaderName { get; init; }

    /// <summary>
    /// Whether speed boost is enabled
    /// </summary>
    [JsonPropertyName("speed_boost")]
    public bool? SpeedBoost { get; init; }

    /// <summary>
    /// Whether Key Phrases is enabled, either true or false
    /// </summary>
    [JsonPropertyName("auto_highlights")]
    public bool AutoHighlights { get; init; }

    [JsonPropertyName("auto_highlights_result")]
    public AutoHighlightsResult? AutoHighlightsResult { get; init; }

    /// <summary>
    /// The point in time, in milliseconds, in the file at which the transcription was started
    /// </summary>
    [JsonPropertyName("audio_start_from")]
    public int? AudioStartFrom { get; init; }

    /// <summary>
    /// The point in time, in milliseconds, in the file at which the transcription was terminated
    /// </summary>
    [JsonPropertyName("audio_end_at")]
    public int? AudioEndAt { get; init; }

    /// <summary>
    /// The list of custom vocabulary to boost transcription probability for
    /// </summary>
    [JsonPropertyName("word_boost")]
    public IEnumerable<string>? WordBoost { get; init; }

    /// <summary>
    /// The word boost parameter value
    /// </summary>
    [JsonPropertyName("boost_param")]
    public string? BoostParam { get; init; }

    /// <summary>
    /// Whether [Profanity Filtering](https://www.assemblyai.com/docs/models/speech-recognition#profanity-filtering) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("filter_profanity")]
    public bool? FilterProfanity { get; init; }

    /// <summary>
    /// Whether [PII Redaction](https://www.assemblyai.com/docs/models/pii-redaction) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("redact_pii")]
    public bool RedactPii { get; init; }

    /// <summary>
    /// Whether a redacted version of the audio file was generated,
    /// either true or false. See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more information.
    /// </summary>
    [JsonPropertyName("redact_pii_audio")]
    public bool? RedactPiiAudio { get; init; }

    [JsonPropertyName("redact_pii_audio_quality")]
    public RedactPiiAudioQuality? RedactPiiAudioQuality { get; init; }

    /// <summary>
    /// The list of PII Redaction policies that were enabled, if PII Redaction is enabled.
    /// See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more information.
    /// </summary>
    [JsonPropertyName("redact_pii_policies")]
    public IEnumerable<PiiPolicy>? RedactPiiPolicies { get; init; }

    /// <summary>
    /// The replacement logic for detected PII, can be "entity_type" or "hash". See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more details.
    /// </summary>
    [JsonPropertyName("redact_pii_sub")]
    public SubstitutionPolicy? RedactPiiSub { get; init; }

    /// <summary>
    /// Whether [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("speaker_labels")]
    public bool? SpeakerLabels { get; init; }

    /// <summary>
    /// Tell the speaker label model how many speakers it should attempt to identify, up to 10. See [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) for more details.
    /// </summary>
    [JsonPropertyName("speakers_expected")]
    public int? SpeakersExpected { get; init; }

    /// <summary>
    /// Whether [Content Moderation](https://www.assemblyai.com/docs/models/content-moderation) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("content_safety")]
    public bool? ContentSafety { get; init; }

    [JsonPropertyName("content_safety_labels")]
    public ContentSafetyLabelsResult? ContentSafetyLabels { get; init; }

    /// <summary>
    /// Whether [Topic Detection](https://www.assemblyai.com/docs/models/topic-detection) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("iab_categories")]
    public bool? IabCategories { get; init; }

    [JsonPropertyName("iab_categories_result")]
    public TopicDetectionModelResult? IabCategoriesResult { get; init; }

    /// <summary>
    /// Whether [Automatic language detection](https://www.assemblyai.com/docs/models/speech-recognition#automatic-language-detection) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("language_detection")]
    public bool? LanguageDetection { get; init; }

    /// <summary>
    /// Customize how words are spelled and formatted using to and from values
    /// </summary>
    [JsonPropertyName("custom_spelling")]
    public IEnumerable<TranscriptCustomSpelling>? CustomSpelling { get; init; }

    /// <summary>
    /// Whether [Auto Chapters](https://www.assemblyai.com/docs/models/auto-chapters) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("auto_chapters")]
    public bool? AutoChapters { get; init; }

    /// <summary>
    /// An array of temporally sequential chapters for the audio file
    /// </summary>
    [JsonPropertyName("chapters")]
    public IEnumerable<Chapter>? Chapters { get; init; }

    /// <summary>
    /// Whether [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("summarization")]
    public bool Summarization { get; init; }

    /// <summary>
    /// The type of summary generated, if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary_type")]
    public string? SummaryType { get; init; }

    /// <summary>
    /// The Summarization model used to generate the summary,
    /// if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary_model")]
    public string? SummaryModel { get; init; }

    /// <summary>
    /// The generated summary of the media file, if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; init; }

    /// <summary>
    /// Whether custom topics is enabled, either true or false
    /// </summary>
    [JsonPropertyName("custom_topics")]
    public bool? CustomTopics { get; init; }

    /// <summary>
    /// The list of custom topics provided if custom topics is enabled
    /// </summary>
    [JsonPropertyName("topics")]
    public IEnumerable<string>? Topics { get; init; }

    /// <summary>
    /// Transcribe Filler Words, like "umm", in your media file; can be true or false
    /// </summary>
    [JsonPropertyName("disfluencies")]
    public bool? Disfluencies { get; init; }

    /// <summary>
    /// Whether [Sentiment Analysis](https://www.assemblyai.com/docs/models/sentiment-analysis) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("sentiment_analysis")]
    public bool? SentimentAnalysis { get; init; }

    /// <summary>
    /// An array of results for the Sentiment Analysis model, if it is enabled.
    /// See [Sentiment Analysis](https://www.assemblyai.com/docs/models/sentiment-analysis) for more information.
    /// </summary>
    [JsonPropertyName("sentiment_analysis_results")]
    public IEnumerable<SentimentAnalysisResult>? SentimentAnalysisResults { get; init; }

    /// <summary>
    /// Whether [Entity Detection](https://www.assemblyai.com/docs/models/entity-detection) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("entity_detection")]
    public bool? EntityDetection { get; init; }

    /// <summary>
    /// An array of results for the Entity Detection model, if it is enabled.
    /// See [Entity detection](https://www.assemblyai.com/docs/models/entity-detection) for more information.
    /// </summary>
    [JsonPropertyName("entities")]
    public IEnumerable<Entity>? Entities { get; init; }

    /// <summary>
    /// Defaults to null. Reject audio files that contain less than this fraction of speech.
    /// Valid values are in the range [0, 1] inclusive.
    /// </summary>
    [JsonPropertyName("speech_threshold")]
    public double? SpeechThreshold { get; init; }

    /// <summary>
    /// True while a request is throttled and false when a request is no longer throttled
    /// </summary>
    [JsonPropertyName("throttled")]
    public bool? Throttled { get; init; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }
}
