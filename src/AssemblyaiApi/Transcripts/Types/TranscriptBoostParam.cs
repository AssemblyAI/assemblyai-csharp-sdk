using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum TranscriptBoostParam
{
    [EnumMember(Value = "low")]
    Low,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "high")]
    High
}
