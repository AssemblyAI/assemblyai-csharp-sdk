using System.Text.Json;
using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class TranscriptParamsTests
{
    [Test]
    public void ShouldMapOptionalToTranscriptParams()
    {
        var optionalParams = new TranscriptOptionalParams
        {
            LanguageCode = TranscriptLanguageCode.En,
            LanguageDetection = true,
            LanguageConfidenceThreshold = 0.9f,
            SpeechModel = SpeechModel.Best,
            Punctuate = true,
            FormatText = true,
            Disfluencies = true,
            DualChannel = true,
            WebhookUrl = "https://example.com/webhook",
            WebhookAuthHeaderName = "Webhook-Authorization",
            WebhookAuthHeaderValue = "passwords",
            AutoHighlights = true,
            AudioStartFrom = 10,
            AudioEndAt = 360,
            WordBoost = ["word"],
            BoostParam = TranscriptBoostParam.Default,
            FilterProfanity = true,
            RedactPii = true,
            RedactPiiAudio = true,
            RedactPiiAudioQuality = RedactPiiAudioQuality.Mp3,
            RedactPiiPolicies = [PiiPolicy.Date, PiiPolicy.Drug],
            RedactPiiSub = SubstitutionPolicy.Hash,
            SpeakerLabels = true,
            SpeakersExpected = 4,
            ContentSafety = true,
            ContentSafetyConfidence = 90,
            IabCategories = true,
            CustomSpelling =
            [
                new TranscriptCustomSpelling
                {
                    From = ["from"],
                    To = "to"
                }
            ],
            SentimentAnalysis = true,
            AutoChapters = true,
            EntityDetection = true,
            SpeechThreshold = 0.9f,
            Summarization = true,
            SummaryModel = SummaryModel.Catchy,
            SummaryType = SummaryType.Bullets,
            CustomTopics = true,
            Topics = ["topic"]
        };
        const string audioUrl = "https://example.com/audio.mp3";
        var transcriptParams = optionalParams.ToTranscriptParams(audioUrl);

        var optionalParamsJson = JsonSerializer.SerializeToElement(optionalParams);
        var transcriptParamsJson = JsonSerializer.SerializeToElement(transcriptParams);
        foreach(var key in optionalParamsJson.EnumerateObject())
        {
            Assert.That(
                transcriptParamsJson.GetProperty(key.Name).ToString(), 
                Is.EqualTo(key.Value.ToString())
            );
        }
    }
}