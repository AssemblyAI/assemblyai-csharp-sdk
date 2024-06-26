using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class ListTranscriptParams
{
    /// <summary>
    /// Maximum amount of transcripts to retrieve
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Filter by transcript status
    /// </summary>
    public TranscriptStatus? Status { get; init; }

    /// <summary>
    /// Only get transcripts created on this date
    /// </summary>
    public string? CreatedOn { get; init; }

    /// <summary>
    /// Get transcripts that were created before this transcript ID
    /// </summary>
    public string? BeforeId { get; init; }

    /// <summary>
    /// Get transcripts that were created after this transcript ID
    /// </summary>
    public string? AfterId { get; init; }

    /// <summary>
    /// Only get throttled transcripts, overrides the status filter
    /// </summary>
    public bool? ThrottledOnly { get; init; }
}
