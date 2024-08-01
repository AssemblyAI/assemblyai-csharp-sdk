using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Transcripts;

public record Chapter
{
    /// <summary>
    /// An ultra-short summary (just a few words) of the content spoken in the chapter
    /// </summary>
    [JsonPropertyName("gist")]
    public required string Gist { get; set; }

    /// <summary>
    /// A single sentence summary of the content spoken during the chapter
    /// </summary>
    [JsonPropertyName("headline")]
    public required string Headline { get; set; }

    /// <summary>
    /// A one paragraph summary of the content spoken during the chapter
    /// </summary>
    [JsonPropertyName("summary")]
    public required string Summary { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, for the chapter
    /// </summary>
    [JsonPropertyName("start")]
    public required int Start { get; set; }

    /// <summary>
    /// The starting time, in milliseconds, for the chapter
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }
}
