using AssemblyAI.Files;
using AssemblyAI.Transcripts;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class TranscriptsClientTests
{
    private const string RemoteAudioUrl = "https://storage.googleapis.com/aai-web-samples/espn-bears.m4a";
    private const string BadRemoteAudioUrl = "https://storage.googleapis.com/aai-web-samples/does-not-exist.m4a";

    private static string TranscriptId => AssemblyAITestParameters.TranscriptId;

    [Test]
    public async Task Should_Submit_Using_Uri()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.SubmitAsync(
            new Uri(RemoteAudioUrl)
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
        });
    }

    [Test]
    public async Task Should_Submit_Using_Stream()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(stream).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
        });
    }

    [Test]
    public async Task Should_Submit_Using_Stream_With_Dispose()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(stream, disposeStream: true).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
        });
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Test]
    public async Task Should_Submit_Using_FileInfo()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileInfo = new FileInfo(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(fileInfo).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
        });
    }


    [Test]
    public async Task Should_Submit_Using_UploadedFile()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.SubmitAsync(new UploadedFile
        {
            UploadUrl = RemoteAudioUrl
        }).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
        });
    }

    [Test]
    public async Task Should_Transcribe_Using_Params()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
            {
                AudioUrl = RemoteAudioUrl
            }
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }


    [Test]
    public async Task Should_Transcribe_Using_Uri()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(
            new Uri(RemoteAudioUrl)
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Transcribe_From_FileInfo()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileInfo = new FileInfo(testFilePath);

        var transcript = await client.Transcripts.TranscribeAsync(fileInfo).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Transcribe_From_UploadedFile()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new UploadedFile
        {
            UploadUrl = RemoteAudioUrl
        }).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Transcribe_Using_Stream()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.TranscribeAsync(stream).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Transcribe_Using_Stream_With_Dispose()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.TranscribeAsync(stream, disposeStream: true, new TranscriptOptionalParams()).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
        
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Test]
    public async Task Should_Fail_To_Transcribe()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new Uri(BadRemoteAudioUrl))
            .ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Null);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Error));
            Assert.That(transcript.Error, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Wait_Until_Ready()
    {
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(stream).ConfigureAwait(false);
        transcript = await client.Transcripts.WaitUntilReady(transcript.Id).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Get_Transcript()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.GetAsync(TranscriptId).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
            Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
        });
    }

    [Test]
    public async Task Should_Delete_Transcript()
    {
        var client = Helpers.CreateClient();

        var transcript = await client.Transcripts.TranscribeAsync(new Uri(RemoteAudioUrl))
            .ConfigureAwait(false);
        transcript = await client.Transcripts.DeleteAsync(transcript.Id).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.EqualTo("Deleted by user."));
            Assert.That(transcript.AudioUrl, Is.EqualTo("http://deleted_by_user"));
        });
    }

    [Test]
    public async Task Should_Paginate_Transcripts()
    {
        var client = Helpers.CreateClient();
        var transcriptPage = await client.Transcripts.ListAsync().ConfigureAwait(false);
        Assert.That(transcriptPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcriptPage.PageDetails.PrevUrl, Is.Not.Null);
            Assert.That(transcriptPage.Transcripts, Is.Not.Empty);
        });

        var prevPage = await client.Transcripts.ListAsync(transcriptPage.PageDetails.PrevUrl);
        Assert.That(prevPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(prevPage.PageDetails.NextUrl, Is.Not.Null);
            Assert.That(prevPage.Transcripts, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Get_Sentences()
    {
        var client = Helpers.CreateClient();
        var sentencesResponse = await client.Transcripts.GetSentencesAsync(TranscriptId)
            .ConfigureAwait(false);

        Assert.That(sentencesResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(sentencesResponse.Id, Is.Not.Empty);
            Assert.That(sentencesResponse.Sentences, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Get_Paragraphs()
    {
        var client = Helpers.CreateClient();
        var paragraphsResponse = await client.Transcripts.GetParagraphsAsync(TranscriptId)
            .ConfigureAwait(false);

        Assert.That(paragraphsResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(paragraphsResponse.Id, Is.Not.Empty);
            Assert.That(paragraphsResponse.Paragraphs, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Get_Srt_Subtitles()
    {
        var client = Helpers.CreateClient();
        var srtSubtitles = await client.Transcripts.GetSubtitlesAsync(
            TranscriptId,
            SubtitleFormat.Srt,
            new GetSubtitlesParams { CharsPerCaption = 32 }
        ).ConfigureAwait(false);

        Assert.That(srtSubtitles, Is.Not.Empty);
    }

    [Test]
    public async Task Should_Get_Vtt_Subtitles()
    {
        var client = Helpers.CreateClient();
        var srtSubtitles = await client.Transcripts.GetSubtitlesAsync(
            TranscriptId,
            SubtitleFormat.Vtt,
            new GetSubtitlesParams { CharsPerCaption = 32 }
        ).ConfigureAwait(false);

        Assert.That(srtSubtitles, Is.Not.Empty);
    }


    [Test]
    [CancelAfter(30000)]
    public async Task Should_Get_Redacted_Audio()
    {
        var client = Helpers.CreateClient();
        var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
        {
            AudioUrl = RemoteAudioUrl,
            RedactPii = true,
            RedactPiiAudio = true,
            RedactPiiAudioQuality = RedactPiiAudioQuality.Mp3,
            RedactPiiPolicies = new[] { PiiPolicy.PersonName },
            RedactPiiSub = SubstitutionPolicy.Hash
        }).ConfigureAwait(false);

        var redactedAudioResponse = await client.Transcripts.GetRedactedAudioAsync(transcript.Id).ConfigureAwait(false);

        var redactedAudioFileStream =
            await client.Transcripts.GetRedactedAudioFileAsync(transcript.Id).ConfigureAwait(false);
        var memoryStream = new MemoryStream();
        await redactedAudioFileStream.CopyToAsync(memoryStream).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(redactedAudioResponse.Status, Is.EqualTo("redacted_audio_ready"));
            Assert.That(redactedAudioResponse.RedactedAudioUrl, Is.Not.Empty);
            Assert.That(memoryStream, Is.Not.Null);
            Assert.That(memoryStream.Length, Is.GreaterThan(0));
        });
    }

    [Test]
    public async Task Should_Word_Search()
    {
        var client = Helpers.CreateClient();
        var searchResponse = await client.Transcripts.WordSearchAsync(
                TranscriptId,
                ["Giants", "Bears"]
            )
            .ConfigureAwait(false);

        Assert.That(searchResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(searchResponse.Id, Is.Not.Empty);
            Assert.That(searchResponse.TotalCount, Is.GreaterThan(0));
            Assert.That(searchResponse.Matches, Is.Not.Empty);
        });
    }
}