using System.Runtime.Serialization;

namespace AssemblyaiApi;

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
