using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum SpeechModel
{
    [EnumMember(Value = "best")]
    Best,

    [EnumMember(Value = "nano")]
    Nano,

    [EnumMember(Value = "conformer-2")]
    Conformer2
}
