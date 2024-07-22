using NUnit.Framework;

namespace AssemblyAI.Test;

[TestFixture]
public class TranscriptTests
{
    [Test]
    public async Task TestTranscript()
    {
        var client = new AssemblyAIClient("");
        var transcript = await client.Transcripts.SubmitAsync(new TranscriptParams
        {
            AudioUrl = "https://storage.googleapis.com/aai-docs-samples/nbc.mp3"
        });
        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
    }
}