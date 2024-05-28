using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum SpeechModel
{
    [EnumMember(Value = "best")]
    Best,

    [EnumMember(Value = "nano")]
    Nano,

    [EnumMember(Value = "conformer-2")]
    Conformer2
}
