using AssemblyAI.Transcripts;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class SpeechRecognitionTests
{
    [Test]
    public async Task Should_Transcribe_Multichannel()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
            {
                AudioUrl = "https://assemblyai-test.s3.us-west-2.amazonaws.com/e2e_tests/en_7dot1_audio_channels.wav",
                Multichannel = true
            }
        ).ConfigureAwait(false);
        var expectedOutput = new[] { "One.", "Two.", "Three.", "Four.", "Five.", "Six.", "Seven.", "Eight." };
        
        Assert.That(transcript.Multichannel, Is.True);
        Assert.That(transcript.Words!, Is.Not.Null);
        Assert.That(transcript.Utterances, Is.Not.Null);
        var words = transcript.Words!.ToList();
        var utterances = transcript.Utterances!.ToList();
        Assert.That(words, Has.Count.EqualTo(expectedOutput.Length));
        Assert.That(utterances, Has.Count.EqualTo(expectedOutput.Length));
        for (var i = 0; i < expectedOutput.Length; i++)
        {
            var channelString = (i + 1).ToString();
            var expectedWord = expectedOutput[i];
            Assert.Multiple(() =>
            {
                Assert.That(words[i].Text, Is.EqualTo(expectedWord));
                Assert.That(words[i].Speaker, Is.EqualTo(channelString));
                Assert.That(words[i].Channel, Is.EqualTo(channelString));
                Assert.That(utterances[i].Text, Is.EqualTo(expectedWord));
                Assert.That(utterances[i].Speaker, Is.EqualTo(channelString));
                Assert.That(utterances[i].Channel, Is.EqualTo(channelString));
                Assert.That(utterances[i].Words.Count(), Is.EqualTo(1));
                var utteranceWord = utterances[i].Words.First();
                Assert.Multiple(() =>
                {
                    Assert.That(utteranceWord.Text, Is.EqualTo(expectedWord));
                    Assert.That(utteranceWord.Speaker, Is.EqualTo(channelString));
                    Assert.That(utteranceWord.Channel, Is.EqualTo(channelString));
                });
            });
        }
    }
}