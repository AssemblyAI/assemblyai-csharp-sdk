using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<SummaryType>))]
public enum SummaryType
{
    [EnumMember(Value = "bullets")]
    Bullets,

    [EnumMember(Value = "bullets_verbose")]
    BulletsVerbose,

    [EnumMember(Value = "gist")]
    Gist,

    [EnumMember(Value = "headline")]
    Headline,

    [EnumMember(Value = "paragraph")]
    Paragraph
}
