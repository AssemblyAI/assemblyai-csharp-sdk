using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum TranscriptReadyStatus
{
    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "error")]
    Error
}
