using System.Text.Json.Serialization;

namespace AssemblyAI;

public class CreateTranscriptOptionalParameters
{
    [JsonPropertyName("language_code")]
    public TranscriptLanguageCode? LanguageCode { get; init; }

    [JsonPropertyName("punctuate")]
    public bool? Punctuate { get; init; }

    [JsonPropertyName("format_text")]
    public bool? FormatText { get; init; }

    [JsonPropertyName("dual_channel")]
    public bool? DualChannel { get; init; }

    [JsonPropertyName("webhook_url")]
    public string? WebhookUrl { get; init; }

    [JsonPropertyName("webhook_auth_header_name")]
    public string? WebhookAuthHeaderName { get; init; }

    [JsonPropertyName("webhook_auth_header_value")]
    public string? WebhookAuthHeaderValue { get; init; }

    [JsonPropertyName("auto_highlights")]
    public bool? AutoHighlights { get; init; }

    [JsonPropertyName("audio_start_from")]
    public int? AudioStartFrom { get; init; }

    [JsonPropertyName("audio_end_at")]
    public int? AudioEndAt { get; init; }

    [JsonPropertyName("word_boost")]
    public IEnumerable<string?> WordBoost { get; init; }

    [JsonPropertyName("boost_param")]
    public TranscriptBoostParam? BoostParam { get; init; }

    [JsonPropertyName("filter_profanity")]
    public bool? FilterProfanity { get; init; }

    [JsonPropertyName("redact_pii")]
    public bool? RedactPii { get; init; }

    [JsonPropertyName("redact_pii_audio")]
    public bool? RedactPiiAudio { get; init; }

    [JsonPropertyName("redact_pii_audio_quality")]
    public string? RedactPiiAudioQuality { get; init; }

    [JsonPropertyName("redact_pii_policies")]
    public IEnumerable<PiiPolicy>? RedactPiiPolicies { get; init; }

    [JsonPropertyName("redact_pii_sub")]
    public SubstitutionPolicy? RedactPiiSub { get; init; }

    [JsonPropertyName("speaker_labels")]
    public bool? SpeakerLabels { get; init; }

    [JsonPropertyName("speakers_expected")]
    public int? SpeakersExpected { get; init; }

    [JsonPropertyName("content_safety")]
    public bool? ContentSafety { get; init; }

    [JsonPropertyName("iab_categories")]
    public bool? IabCategories { get; init; }

    [JsonPropertyName("language_detection")]
    public bool? LanguageDetection { get; init; }

    [JsonPropertyName("custom_spelling")]
    public IEnumerable<TranscriptCustomSpelling>? CustomSpelling { get; init; }

    [JsonPropertyName("disfluencies")]
    public bool? Disfluencies { get; init; }

    [JsonPropertyName("sentiment_analysis")]
    public bool? SentimentAnalysis { get; init; }

    [JsonPropertyName("auto_chapters")]
    public bool? AutoChapters { get; init; }

    [JsonPropertyName("entity_detection")]
    public bool? EntityDetection { get; init; }

    [JsonPropertyName("speech_threshold")]
    public double? SpeechThreshold { get; init; }

    [JsonPropertyName("summarization")]
    public bool? Summarization { get; init; }

    [JsonPropertyName("summary_model")]
    public SummaryModel? SummaryModel { get; init; }

    [JsonPropertyName("summary_type")]
    public SummaryType? SummaryType { get; init; }

    [JsonPropertyName("custom_topics")]
    public bool? CustomTopics { get; init; }

    [JsonPropertyName("topics")]
    public IEnumerable<string?> Topics { get; init; }
}