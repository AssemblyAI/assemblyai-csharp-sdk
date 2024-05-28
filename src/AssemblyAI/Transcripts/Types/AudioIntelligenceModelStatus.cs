using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum AudioIntelligenceModelStatus
{
    [EnumMember(Value = "success")]
    Success,

    [EnumMember(Value = "unavailable")]
    Unavailable
}
