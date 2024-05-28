using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum TranscriptReadyStatus
{
    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "error")]
    Error
}
