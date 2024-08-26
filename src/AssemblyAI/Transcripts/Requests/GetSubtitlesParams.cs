using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record GetSubtitlesParams
{
    /// <summary>
    /// The maximum number of characters per caption
    /// </summary>
    public int? CharsPerCaption { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
