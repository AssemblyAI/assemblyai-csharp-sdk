using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum TranscriptStatus
{
    [EnumMember(Value = "queued")]
    Queued,

    [EnumMember(Value = "processing")]
    Processing,

    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "error")]
    Error
}
