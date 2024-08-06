#nullable enable

// ReSharper disable once CheckNamespace
namespace AssemblyAI.Transcripts;

public static class TranscriptExtensions
{
    /// <summary>
    /// Throws an exception if the transcript status is not completed.
    /// </summary>
    /// <param name="transcript">The transcript</param>
    /// <exception cref="TranscriptNotCompletedStatusException">
    /// The exception is thrown if the transcript status is not completed.
    /// The exception contains the transcript, so you can access the status and error message.
    /// </exception>
    public static void EnsureStatusCompleted(this Transcript transcript)
    {
        switch (transcript.Status)
        {
            case TranscriptStatus.Completed:
                return;
            case TranscriptStatus.Error:
                throw new TranscriptNotCompletedStatusException(
                    transcript,
                    $"Transcript status is {transcript.Status}, with error message '{transcript.Error}'."
                );
            case TranscriptStatus.Queued:
            case TranscriptStatus.Processing:
            default:
                throw new TranscriptNotCompletedStatusException(
                    transcript,
                    $"Transcript status is {transcript.Status}, not completed."
                );
        }
    }
}
