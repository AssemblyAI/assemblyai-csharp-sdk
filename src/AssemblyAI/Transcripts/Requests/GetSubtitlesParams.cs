namespace AssemblyAI.Transcripts;

public record GetSubtitlesParams
{
    /// <summary>
    /// The maximum number of characters per caption
    /// </summary>
    public int? CharsPerCaption { get; set; }
}
