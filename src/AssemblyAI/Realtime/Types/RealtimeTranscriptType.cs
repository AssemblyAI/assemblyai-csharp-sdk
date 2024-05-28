using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum RealtimeTranscriptType
{
    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript
}
