using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public record WordSearchMatch
{
    /// <summary>
    /// The matched word
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The total amount of times the word is in the transcript
    /// </summary>
    [JsonPropertyName("count")]
    public required int Count { get; set; }

    /// <summary>
    /// An array of timestamps
    /// </summary>
    [JsonPropertyName("timestamps")]
    public IEnumerable<IEnumerable<int>> Timestamps { get; set; } = new List<IEnumerable<int>>();

    /// <summary>
    /// An array of all index locations for that word within the `words` array of the completed transcript
    /// </summary>
    [JsonPropertyName("indexes")]
    public IEnumerable<int> Indexes { get; set; } = new List<int>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
