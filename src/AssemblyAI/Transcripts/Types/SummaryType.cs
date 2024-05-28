using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

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
