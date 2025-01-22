using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<SubtitleFormat>))]
public enum SubtitleFormat
{
    [EnumMember(Value = "srt")]
    Srt,

    [EnumMember(Value = "vtt")]
    Vtt,
}
