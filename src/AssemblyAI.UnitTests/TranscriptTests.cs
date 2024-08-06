using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class TranscriptTests
{
    [Test]
    public void ShouldThrowExceptionIfTranscriptStatusIsNotCompleted()
    {
        var transcript = new Transcript
        {
            Status = TranscriptStatus.Queued,
            Id = null!,
            LanguageModel = null!,
            AcousticModel = null!,
            AudioUrl = null!,
            WebhookAuth = false,
            AutoHighlights = false,
            RedactPii = false,
            Summarization = false
        };
        Assert.Throws<TranscriptNotCompletedStatusException>(() => transcript.EnsureStatusCompleted());
    }
    
    [Test]
    public void ShouldThrowExceptionWithErrorIfTranscriptStatusIsNotCompleted()
    {
        const string errorMessage = "An error occurred.";
        var transcript = new Transcript
        {
            Status = TranscriptStatus.Error,
            Error = errorMessage,
            Id = null!,
            LanguageModel = null!,
            AcousticModel = null!,
            AudioUrl = null!,
            WebhookAuth = false,
            AutoHighlights = false,
            RedactPii = false,
            Summarization = false
        };
        var ex = Assert.Throws<TranscriptNotCompletedStatusException>(() => transcript.EnsureStatusCompleted());
        Assert.That(ex.Message, Does.Contain(errorMessage));
    }

    public async Task Test()
    {
        
        var client = new AssemblyAIClient(Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!);

        // Transcribe file at remote URL
        var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
        {
            AudioUrl = "https://storage.googleapis.com/aai-web-samples/espn-bears.m4a",
            LanguageCode = TranscriptLanguageCode.EnUs
        });

// checks if transcript.Status == TranscriptStatus.Completed, throws an exception if not
transcript.EnsureStatusCompleted();

        Console.WriteLine(transcript.Text);
    }
}