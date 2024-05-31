using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<RealtimeTranscriptType>))]
public enum RealtimeTranscriptType
{
    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript
}
