using AssemblyAI.Lemur;
using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class EnumConverterTests
{
    [Test]
    public void ConvertStringToEnum()
    {
        var result = EnumConverter.ToEnum<TranscriptLanguageCode>("en_us");
        Assert.That(result, Is.EqualTo(TranscriptLanguageCode.EnUs));
    }
    
    [Test]
    public void ConvertEnumToString()
    {
        var result = EnumConverter.ToString(TranscriptLanguageCode.EnUs);
        Assert.That(result, Is.EqualTo("en_us"));
    }
    
    [Test]
    public void ConvertStringToClaude2_0()
    {
        var result = EnumConverter.ToEnum<LemurModel>("anthropic/claude-2");
        Assert.That(result, Is.EqualTo(LemurModel.AnthropicClaude2_0));
#pragma warning disable CS0618 // Type or member is obsolete
        Assert.That(result, Is.Not.EqualTo(LemurModel.AnthropicClaude2));
#pragma warning restore CS0618 // Type or member is obsolete
    }
    
    [Test]
    public void ConvertClaude2_0ToString()
    {
        var result1 = EnumConverter.ToString(LemurModel.AnthropicClaude2_0);
#pragma warning disable CS0618 // Type or member is obsolete
        var result2 = EnumConverter.ToString(LemurModel.AnthropicClaude2);
#pragma warning restore CS0618 // Type or member is obsolete
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.EqualTo("anthropic/claude-2"));
            Assert.That(result2, Is.EqualTo("anthropic/claude-2"));
        });
    }
}