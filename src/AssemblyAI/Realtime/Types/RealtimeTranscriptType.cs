using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Realtime;

#nullable enable

namespace AssemblyAI.Realtime;

[JsonConverter(typeof(StringEnumSerializer<RealtimeTranscriptType>))]
public enum RealtimeTranscriptType
{
    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript
}
