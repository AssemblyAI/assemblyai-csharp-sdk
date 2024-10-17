using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public partial record Transcript
{
    /// <summary>
    /// The unique identifier of your transcript
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The URL of the media that was transcribed
    /// </summary>
    [JsonPropertyName("audio_url")]
    public required string AudioUrl { get; set; }

    /// <summary>
    /// The status of your transcript. Possible values are queued, processing, completed, or error.
    /// </summary>
    [JsonPropertyName("status")]
    public required TranscriptStatus Status { get; set; }

    /// <summary>
    /// The language of your audio file.
    /// Possible values are found in [Supported Languages](https://www.assemblyai.com/docs/concepts/supported-languages).
    /// The default value is 'en_us'.
    /// </summary>
    [JsonPropertyName("language_code")]
    public TranscriptLanguageCode? LanguageCode { get; set; }

    /// <summary>
    /// Whether [Automatic language detection](https://www.assemblyai.com/docs/models/speech-recognition#automatic-language-detection) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("language_detection")]
    public bool? LanguageDetection { get; set; }

    /// <summary>
    /// The confidence threshold for the automatically detected language.
    /// An error will be returned if the language confidence is below this threshold.
    /// </summary>
    [JsonPropertyName("language_confidence_threshold")]
    public float? LanguageConfidenceThreshold { get; set; }

    /// <summary>
    /// The confidence score for the detected language, between 0.0 (low confidence) and 1.0 (high confidence)
    /// </summary>
    [JsonPropertyName("language_confidence")]
    public double? LanguageConfidence { get; set; }

    [JsonPropertyName("speech_model")]
    public SpeechModel? SpeechModel { get; set; }

    /// <summary>
    /// The textual transcript of your media file
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// An array of temporally-sequential word objects, one for each word in the transcript.
    /// See [Speech recognition](https://www.assemblyai.com/docs/models/speech-recognition) for more information.
    /// </summary>
    [JsonPropertyName("words")]
    public IEnumerable<TranscriptWord>? Words { get; set; }

    /// <summary>
    /// When dual_channel or speaker_labels is enabled, a list of turn-by-turn utterance objects.
    /// See [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) for more information.
    /// </summary>
    [JsonPropertyName("utterances")]
    public IEnumerable<TranscriptUtterance>? Utterances { get; set; }

    /// <summary>
    /// The confidence score for the transcript, between 0.0 (low confidence) and 1.0 (high confidence)
    /// </summary>
    [JsonPropertyName("confidence")]
    public double? Confidence { get; set; }

    /// <summary>
    /// The duration of this transcript object's media file, in seconds
    /// </summary>
    [JsonPropertyName("audio_duration")]
    public int? AudioDuration { get; set; }

    /// <summary>
    /// Whether Automatic Punctuation is enabled, either true or false
    /// </summary>
    [JsonPropertyName("punctuate")]
    public bool? Punctuate { get; set; }

    /// <summary>
    /// Whether Text Formatting is enabled, either true or false
    /// </summary>
    [JsonPropertyName("format_text")]
    public bool? FormatText { get; set; }

    /// <summary>
    /// Transcribe Filler Words, like "umm", in your media file; can be true or false
    /// </summary>
    [JsonPropertyName("disfluencies")]
    public bool? Disfluencies { get; set; }

    /// <summary>
    /// Whether [Multichannel transcription](https://www.assemblyai.com/docs/models/speech-recognition#multichannel-transcription) was enabled in the transcription request, either true or false
    /// </summary>
    [JsonPropertyName("multichannel")]
    public bool? Multichannel { get; set; }

    /// <summary>
    /// The number of audio channels in the audio file. This is only present when multichannel is enabled.
    /// </summary>
    [JsonPropertyName("audio_channels")]
    public int? AudioChannels { get; set; }

    /// <summary>
    /// Whether [Dual channel transcription](https://www.assemblyai.com/docs/models/speech-recognition#dual-channel-transcription) was enabled in the transcription request, either true or false
    /// </summary>
    [JsonPropertyName("dual_channel")]
    public bool? DualChannel { get; set; }

    /// <summary>
    /// The URL to which we send webhook requests.
    /// We sends two different types of webhook requests.
    /// One request when a transcript is completed or failed, and one request when the redacted audio is ready if redact_pii_audio is enabled.
    /// </summary>
    [JsonPropertyName("webhook_url")]
    public string? WebhookUrl { get; set; }

    /// <summary>
    /// The status code we received from your server when delivering the transcript completed or failed webhook request, if a webhook URL was provided
    /// </summary>
    [JsonPropertyName("webhook_status_code")]
    public int? WebhookStatusCode { get; set; }

    /// <summary>
    /// Whether webhook authentication details were provided
    /// </summary>
    [JsonPropertyName("webhook_auth")]
    public required bool WebhookAuth { get; set; }

    /// <summary>
    /// The header name to be sent with the transcript completed or failed webhook requests
    /// </summary>
    [JsonPropertyName("webhook_auth_header_name")]
    public string? WebhookAuthHeaderName { get; set; }

    /// <summary>
    /// Whether speed boost is enabled
    /// </summary>
    [JsonPropertyName("speed_boost")]
    public bool? SpeedBoost { get; set; }

    /// <summary>
    /// Whether Key Phrases is enabled, either true or false
    /// </summary>
    [JsonPropertyName("auto_highlights")]
    public required bool AutoHighlights { get; set; }

    [JsonPropertyName("auto_highlights_result")]
    public AutoHighlightsResult? AutoHighlightsResult { get; set; }

    /// <summary>
    /// The point in time, in milliseconds, in the file at which the transcription was started
    /// </summary>
    [JsonPropertyName("audio_start_from")]
    public int? AudioStartFrom { get; set; }

    /// <summary>
    /// The point in time, in milliseconds, in the file at which the transcription was terminated
    /// </summary>
    [JsonPropertyName("audio_end_at")]
    public int? AudioEndAt { get; set; }

    /// <summary>
    /// The list of custom vocabulary to boost transcription probability for
    /// </summary>
    [JsonPropertyName("word_boost")]
    public IEnumerable<string>? WordBoost { get; set; }

    /// <summary>
    /// The word boost parameter value
    /// </summary>
    [JsonPropertyName("boost_param")]
    public string? BoostParam { get; set; }

    /// <summary>
    /// Whether [Profanity Filtering](https://www.assemblyai.com/docs/models/speech-recognition#profanity-filtering) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("filter_profanity")]
    public bool? FilterProfanity { get; set; }

    /// <summary>
    /// Whether [PII Redaction](https://www.assemblyai.com/docs/models/pii-redaction) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("redact_pii")]
    public required bool RedactPii { get; set; }

    /// <summary>
    /// Whether a redacted version of the audio file was generated,
    /// either true or false. See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more information.
    /// </summary>
    [JsonPropertyName("redact_pii_audio")]
    public bool? RedactPiiAudio { get; set; }

    [JsonPropertyName("redact_pii_audio_quality")]
    public RedactPiiAudioQuality? RedactPiiAudioQuality { get; set; }

    /// <summary>
    /// The list of PII Redaction policies that were enabled, if PII Redaction is enabled.
    /// See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more information.
    /// </summary>
    [JsonPropertyName("redact_pii_policies")]
    public IEnumerable<PiiPolicy>? RedactPiiPolicies { get; set; }

    /// <summary>
    /// The replacement logic for detected PII, can be "entity_type" or "hash". See [PII redaction](https://www.assemblyai.com/docs/models/pii-redaction) for more details.
    /// </summary>
    [JsonPropertyName("redact_pii_sub")]
    public SubstitutionPolicy? RedactPiiSub { get; set; }

    /// <summary>
    /// Whether [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("speaker_labels")]
    public bool? SpeakerLabels { get; set; }

    /// <summary>
    /// Tell the speaker label model how many speakers it should attempt to identify, up to 10. See [Speaker diarization](https://www.assemblyai.com/docs/models/speaker-diarization) for more details.
    /// </summary>
    [JsonPropertyName("speakers_expected")]
    public int? SpeakersExpected { get; set; }

    /// <summary>
    /// Whether [Content Moderation](https://www.assemblyai.com/docs/models/content-moderation) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("content_safety")]
    public bool? ContentSafety { get; set; }

    [JsonPropertyName("content_safety_labels")]
    public ContentSafetyLabelsResult? ContentSafetyLabels { get; set; }

    /// <summary>
    /// Whether [Topic Detection](https://www.assemblyai.com/docs/models/topic-detection) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("iab_categories")]
    public bool? IabCategories { get; set; }

    [JsonPropertyName("iab_categories_result")]
    public TopicDetectionModelResult? IabCategoriesResult { get; set; }

    /// <summary>
    /// Customize how words are spelled and formatted using to and from values
    /// </summary>
    [JsonPropertyName("custom_spelling")]
    public IEnumerable<TranscriptCustomSpelling>? CustomSpelling { get; set; }

    /// <summary>
    /// Whether [Auto Chapters](https://www.assemblyai.com/docs/models/auto-chapters) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("auto_chapters")]
    public bool? AutoChapters { get; set; }

    /// <summary>
    /// An array of temporally sequential chapters for the audio file
    /// </summary>
    [JsonPropertyName("chapters")]
    public IEnumerable<Chapter>? Chapters { get; set; }

    /// <summary>
    /// Whether [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled, either true or false
    /// </summary>
    [JsonPropertyName("summarization")]
    public required bool Summarization { get; set; }

    /// <summary>
    /// The type of summary generated, if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary_type")]
    public string? SummaryType { get; set; }

    /// <summary>
    /// The Summarization model used to generate the summary,
    /// if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary_model")]
    public string? SummaryModel { get; set; }

    /// <summary>
    /// The generated summary of the media file, if [Summarization](https://www.assemblyai.com/docs/models/summarization) is enabled
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>
    /// Whether custom topics is enabled, either true or false
    /// </summary>
    [JsonPropertyName("custom_topics")]
    public bool? CustomTopics { get; set; }

    /// <summary>
    /// The list of custom topics provided if custom topics is enabled
    /// </summary>
    [JsonPropertyName("topics")]
    public IEnumerable<string>? Topics { get; set; }

    /// <summary>
    /// Whether [Sentiment Analysis](https://www.assemblyai.com/docs/models/sentiment-analysis) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("sentiment_analysis")]
    public bool? SentimentAnalysis { get; set; }

    /// <summary>
    /// An array of results for the Sentiment Analysis model, if it is enabled.
    /// See [Sentiment Analysis](https://www.assemblyai.com/docs/models/sentiment-analysis) for more information.
    /// </summary>
    [JsonPropertyName("sentiment_analysis_results")]
    public IEnumerable<SentimentAnalysisResult>? SentimentAnalysisResults { get; set; }

    /// <summary>
    /// Whether [Entity Detection](https://www.assemblyai.com/docs/models/entity-detection) is enabled, can be true or false
    /// </summary>
    [JsonPropertyName("entity_detection")]
    public bool? EntityDetection { get; set; }

    /// <summary>
    /// An array of results for the Entity Detection model, if it is enabled.
    /// See [Entity detection](https://www.assemblyai.com/docs/models/entity-detection) for more information.
    /// </summary>
    [JsonPropertyName("entities")]
    public IEnumerable<Entity>? Entities { get; set; }

    /// <summary>
    /// Defaults to null. Reject audio files that contain less than this fraction of speech.
    /// Valid values are in the range [0, 1] inclusive.
    /// </summary>
    [JsonPropertyName("speech_threshold")]
    public float? SpeechThreshold { get; set; }

    /// <summary>
    /// True while a request is throttled and false when a request is no longer throttled
    /// </summary>
    [JsonPropertyName("throttled")]
    public bool? Throttled { get; set; }

    /// <summary>
    /// Error message of why the transcript failed
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// The language model that was used for the transcript
    /// </summary>
    [JsonPropertyName("language_model")]
    public required string LanguageModel { get; set; }

    /// <summary>
    /// The acoustic model that was used for the transcript
    /// </summary>
    [JsonPropertyName("acoustic_model")]
    public required string AcousticModel { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
