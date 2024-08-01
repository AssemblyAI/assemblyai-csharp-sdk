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
}