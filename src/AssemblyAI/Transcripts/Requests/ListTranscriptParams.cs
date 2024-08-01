using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

public record ListTranscriptParams
{
    /// <summary>
    /// Maximum amount of transcripts to retrieve
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Filter by transcript status
    /// </summary>
    public TranscriptStatus? Status { get; set; }

    /// <summary>
    /// Only get transcripts created on this date
    /// </summary>
    public string? CreatedOn { get; set; }

    /// <summary>
    /// Get transcripts that were created before this transcript ID
    /// </summary>
    public string? BeforeId { get; set; }

    /// <summary>
    /// Get transcripts that were created after this transcript ID
    /// </summary>
    public string? AfterId { get; set; }

    /// <summary>
    /// Only get throttled transcripts, overrides the status filter
    /// </summary>
    public bool? ThrottledOnly { get; set; }
}
