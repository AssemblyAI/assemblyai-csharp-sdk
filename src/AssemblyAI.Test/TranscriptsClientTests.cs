using NUnit.Framework;

namespace AssemblyAI.Test;

[TestFixture]
public class TranscriptsClientTests
{
    private string _apiKey;

    [SetUp]
    public void Setup()
    {
        // Retrieve the API key from the .runsettings file
        _apiKey = TestContext.Parameters.Get("ASSEMBLYAI_API_KEY");
        if(string.IsNullOrEmpty(_apiKey)) throw new Exception("ASSEMBLYAI_API_KEY .runsettings parameter is not set.");
    }
    
    [Test]
    public async Task Should_Submit_Using_Uri()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        var transcript = await client.Transcripts.SubmitAsync(
            new Uri("https://storage.googleapis.com/aai-docs-samples/nbc.mp3")
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
    }
    
    [Test]
    public async Task Should_Submit_Using_Stream()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        await using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(stream).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
    }
    
    [Test]
    public async Task Should_Submit_Using_FileInfo()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileInfo = new FileInfo(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(fileInfo).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
    }
    
    [Test]
    public async Task Should_Transcribe_Using_Uri()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        var transcript = await client.Transcripts.TranscribeAsync(
            new Uri("https://storage.googleapis.com/aai-docs-samples/nbc.mp3")
        ).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Queued));
    }
    
    [Test]
    public async Task Should_Transcribe_From_FileInfo()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileInfo = new FileInfo(testFilePath);

        var transcript = await client.Transcripts.TranscribeAsync(fileInfo).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
    }
    
    [Test]
    public async Task Should_Transcribe_Using_Stream()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        await using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.TranscribeAsync(stream).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
    }
    
    [Test]
    public async Task Should_Wait_Until_Ready()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        await using var stream = File.OpenRead(testFilePath);

        var transcript = await client.Transcripts.SubmitAsync(stream).ConfigureAwait(false);
        transcript = await client.Transcripts.WaitUntilReady(transcript.Id).ConfigureAwait(false);

        Assert.That(transcript, Is.Not.Null);
        Assert.That(transcript.Id, Is.Not.Null);
        Assert.That(transcript.Status, Is.EqualTo(TranscriptStatus.Completed));
    }

    [Test]
    public async Task Should_Paginate_Transcripts()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);
        var transcriptPage = await client.Transcripts.ListAsync().ConfigureAwait(false);
        Assert.That(transcriptPage, Is.Not.Null);
        Assert.That(transcriptPage.PageDetails.PrevUrl, Is.Not.Null);
        Assert.That(transcriptPage.Transcripts, Is.Not.Empty);

        var prevPage = await client.Transcripts.ListAsync(transcriptPage.PageDetails.PrevUrl);
        Assert.That(prevPage, Is.Not.Null);
        Assert.That(prevPage.PageDetails.NextUrl, Is.Not.Null);
        Assert.That(prevPage.Transcripts, Is.Not.Empty);
    }
}