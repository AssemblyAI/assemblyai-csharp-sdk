namespace AssemblyAI.Transcripts;

public record WordSearchParams
{
    /// <summary>
    /// Keywords to search for
    /// </summary>
    public IEnumerable<string> Words { get; set; } = new List<string>();
}
