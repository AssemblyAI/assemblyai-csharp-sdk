using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum SubtitleFormat
{
    [EnumMember(Value = "srt")]
    Srt,

    [EnumMember(Value = "vtt")]
    Vtt
}
