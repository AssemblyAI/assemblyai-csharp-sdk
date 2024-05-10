using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum SubtitleFormat
{
    [EnumMember(Value = "srt")]
    Srt,

    [EnumMember(Value = "vtt")]
    Vtt
}
