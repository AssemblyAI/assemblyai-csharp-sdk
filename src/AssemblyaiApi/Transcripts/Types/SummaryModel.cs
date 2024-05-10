using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum SummaryModel
{
    [EnumMember(Value = "informative")]
    Informative,

    [EnumMember(Value = "conversational")]
    Conversational,

    [EnumMember(Value = "catchy")]
    Catchy
}
