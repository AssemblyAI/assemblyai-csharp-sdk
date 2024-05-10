using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum RealtimeTranscriptType
{
    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript
}
