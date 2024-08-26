using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record WordSearchParams
{
    /// <summary>
    /// Keywords to search for
    /// </summary>
    public IEnumerable<string> Words { get; set; } = new List<string>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
