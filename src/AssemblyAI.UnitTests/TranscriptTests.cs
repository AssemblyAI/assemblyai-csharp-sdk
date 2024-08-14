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
}