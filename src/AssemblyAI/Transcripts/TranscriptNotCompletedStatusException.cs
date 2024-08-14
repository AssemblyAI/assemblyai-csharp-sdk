// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AssemblyAI.Transcripts;

/// <summary>
/// Exception thrown when a transcript status is not completed.
/// </summary>
public class TranscriptNotCompletedStatusException : Exception
{
    /// <summary>
    /// The transcript that caused the exception.
    /// </summary>
    public Transcript Transcript { get; private init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TranscriptNotCompletedStatusException"/> class.
    /// </summary>
    /// <param name="transcript">The transcript that caused the exception.</param>
    /// <param name="message">Exception message</param>
    internal TranscriptNotCompletedStatusException(Transcript transcript, string message) : base(message)
    {
        Transcript = transcript;
    }
}