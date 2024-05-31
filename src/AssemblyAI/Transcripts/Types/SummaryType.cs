using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

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
