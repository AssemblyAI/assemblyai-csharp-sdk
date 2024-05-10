using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum AudioIntelligenceModelStatus
{
    [EnumMember(Value = "success")]
    Success,

    [EnumMember(Value = "unavailable")]
    Unavailable
}
