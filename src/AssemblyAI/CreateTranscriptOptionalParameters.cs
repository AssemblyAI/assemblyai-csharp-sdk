using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssemblyAI
{
    public class CreateTranscriptOptionalParameters
    {
        [JsonPropertyName("language_code")]
        public TranscriptLanguageCode LanguageCode { get; set; }

        [JsonPropertyName("punctuate")]
        public bool? Punctuate { get; set; }

        [JsonPropertyName("format_text")]
        public bool? FormatText { get; set; }

        [JsonPropertyName("dual_channel")]
        public bool? DualChannel { get; set; }

        [JsonPropertyName("webhook_url")]
        public string WebhookUrl { get; set; }

        [JsonPropertyName("webhook_auth_header_name")]
        public string WebhookAuthHeaderName { get; set; }

        [JsonPropertyName("webhook_auth_header_value")]
        public string WebhookAuthHeaderValue { get; set; }

        [JsonPropertyName("auto_highlights")]
        public bool? AutoHighlights { get; set; }

        [JsonPropertyName("audio_start_from")]
        public int? AudioStartFrom { get; set; }

        [JsonPropertyName("audio_end_at")]
        public int? AudioEndAt { get; set; }

        [JsonPropertyName("word_boost")]
        public IEnumerable<string> WordBoost { get; set; }

        [JsonPropertyName("boost_param")]
        public TranscriptBoostParam BoostParam { get; set; }

        [JsonPropertyName("filter_profanity")]
        public bool? FilterProfanity { get; set; }

        [JsonPropertyName("redact_pii")]
        public bool? RedactPii { get; set; }

        [JsonPropertyName("redact_pii_audio")]
        public bool? RedactPiiAudio { get; set; }

        [JsonPropertyName("redact_pii_audio_quality")]
        public string RedactPiiAudioQuality { get; set; }

        [JsonPropertyName("redact_pii_policies")]
        public IEnumerable<PiiPolicy> RedactPiiPolicies { get; set; }

        [JsonPropertyName("redact_pii_sub")]
        public SubstitutionPolicy RedactPiiSub { get; set; }

        [JsonPropertyName("speaker_labels")]
        public bool? SpeakerLabels { get; set; }

        [JsonPropertyName("speakers_expected")]
        public int? SpeakersExpected { get; set; }

        [JsonPropertyName("content_safety")]
        public bool? ContentSafety { get; set; }

        [JsonPropertyName("iab_categories")]
        public bool? IabCategories { get; set; }

        [JsonPropertyName("language_detection")]
        public bool? LanguageDetection { get; set; }

        [JsonPropertyName("custom_spelling")]
        public IEnumerable<TranscriptCustomSpelling> CustomSpelling { get; set; }

        [JsonPropertyName("disfluencies")]
        public bool? Disfluencies { get; set; }

        [JsonPropertyName("sentiment_analysis")]
        public bool? SentimentAnalysis { get; set; }

        [JsonPropertyName("auto_chapters")]
        public bool? AutoChapters { get; set; }

        [JsonPropertyName("entity_detection")]
        public bool? EntityDetection { get; set; }

        [JsonPropertyName("speech_threshold")]
        public double? SpeechThreshold { get; set; }

        [JsonPropertyName("summarization")]
        public bool? Summarization { get; set; }

        [JsonPropertyName("summary_model")]
        public SummaryModel SummaryModel { get; set; }

        [JsonPropertyName("summary_type")]
        public SummaryType SummaryType { get; set; }

        [JsonPropertyName("custom_topics")]
        public bool? CustomTopics { get; set; }

        [JsonPropertyName("topics")]
        public IEnumerable<string> Topics { get; set; }
    }
}