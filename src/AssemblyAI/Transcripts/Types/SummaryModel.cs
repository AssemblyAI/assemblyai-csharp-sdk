using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum SummaryModel
{
    [EnumMember(Value = "informative")]
    Informative,

    [EnumMember(Value = "conversational")]
    Conversational,

    [EnumMember(Value = "catchy")]
    Catchy
}
