using AssemblyAI.Transcripts;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class AudioIntelligenceTests
{
    private const string RemoteAudioUrl = "https://assembly.ai/espn.m4a";

    [Test]
    public async Task ShouldReturnContentSafety()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
            {
                AudioUrl = RemoteAudioUrl,
                ContentSafety = true,
                ContentSafetyConfidence = 50,
            }
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
            Assert.That(transcript.ContentSafetyLabels, Is.Not.Null);
            Assert.Multiple(() =>
                {
                    Assert.That(
                        transcript.ContentSafetyLabels!.Status,
                        Is.EqualTo(AudioIntelligenceModelStatus.Success)
                    );
                    Assert.That(
                        transcript.ContentSafetyLabels!.Results,
                        Is.Not.Empty
                    );
                    var result = transcript.ContentSafetyLabels!.Results.First();
                    Assert.That(result.Text, Is.Not.Empty);
                    Assert.That(result.Labels, Is.Not.Empty);
                }
            );
        });
    }
}