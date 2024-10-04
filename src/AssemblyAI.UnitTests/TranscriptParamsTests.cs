using System.Text.Json;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class TranscriptParamsTests
{
    private static readonly TranscriptOptionalParams OptionalParams = new()
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
        var transcriptParams = OptionalParams.ToTranscriptParams(audioUrl);

        var optionalParamsJson = JsonUtils.SerializeToElement(OptionalParams);
        var transcriptParamsJson = JsonUtils.SerializeToElement(transcriptParams);
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
        var clonedParams = OptionalParams.Clone();
        var paramsJson = JsonUtils.Serialize(OptionalParams);
        var cloneJson = JsonUtils.Serialize(clonedParams);
        Assert.That(paramsJson, Is.EqualTo(cloneJson));
    }
    
    [Test]
    public void ShouldSerializeExtensionData()
    {
        var paramsJson = JsonUtils.SerializeToElement(new TranscriptParams
        {
            AudioUrl = "https://example.com/audio.mp3",
            ExtensionData = new Dictionary<string, object>()
            {
                ["foo"] = new
                {
                    bar = "baz"
                }
            }
        });
        Assert.That(paramsJson.GetProperty("foo").GetProperty("bar").GetString(), Is.EqualTo("baz"));
        TestContext.WriteLine($"Params JSON with extension data: {paramsJson}");
    }
    
    [Test]
    public void ShouldNotSerializeNullExtensionData()
    {
        var paramsJson = JsonUtils.SerializeToElement(new TranscriptParams
        {
            AudioUrl = "https://example.com/audio.mp3"
        });
        Assert.That(paramsJson.EnumerateObject().Count(), Is.EqualTo(1), "Null data should not be serialized");
        TestContext.WriteLine($"Params JSON without extension data: {paramsJson}");
    }
    
    [Test]
    public void ShouldNotSerializeEmptyExtensionData()
    {
        var paramsJson = JsonUtils.SerializeToElement(new TranscriptParams
        {
            AudioUrl = "https://example.com/audio.mp3",
            ExtensionData = new Dictionary<string, object>()
        });
        Assert.That(paramsJson.EnumerateObject().Count(), Is.EqualTo(1), "Null data should not be serialized");
        TestContext.WriteLine($"Params JSON without extension data: {paramsJson}");
    }
}