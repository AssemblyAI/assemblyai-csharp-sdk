using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum TranscriptBoostParam
{
    [EnumMember(Value = "low")]
    Low,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "high")]
    High
}
