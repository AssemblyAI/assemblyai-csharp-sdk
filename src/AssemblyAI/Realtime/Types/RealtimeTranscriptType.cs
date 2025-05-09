using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

[JsonConverter(typeof(EnumSerializer<RealtimeTranscriptType>))]
public enum RealtimeTranscriptType
{
    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript,
}
