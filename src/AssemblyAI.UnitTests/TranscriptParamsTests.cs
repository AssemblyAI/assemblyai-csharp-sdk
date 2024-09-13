using System.Text.Json;
using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class TranscriptParamsTests
{
    private static TranscriptOptionalParams _optionalParams = new()
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

    [Test]
    public void ShouldMapOptionalToTranscriptParams()
    {
        const string audioUrl = "https://example.com/audio.mp3";
        var transcriptParams = _optionalParams.ToTranscriptParams(audioUrl);

        var optionalParamsJson = JsonSerializer.SerializeToElement(_optionalParams);
        var transcriptParamsJson = JsonSerializer.SerializeToElement(transcriptParams);
        foreach(var key in optionalParamsJson.EnumerateObject())
        {
            Assert.That(
                transcriptParamsJson.GetProperty(key.Name).ToString(), 
                Is.EqualTo(key.Value.ToString())
            );
        }
    }

    [Test]
    public void ShouldClone()
    {
        var clonedParams = _optionalParams.Clone();
        var paramsJson = JsonSerializer.Serialize(_optionalParams);
        var cloneJson = JsonSerializer.Serialize(clonedParams);
        Assert.That(paramsJson, Is.EqualTo(cloneJson));
    }
}