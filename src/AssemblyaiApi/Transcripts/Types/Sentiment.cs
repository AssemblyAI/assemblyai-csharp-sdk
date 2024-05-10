using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum Sentiment
{
    [EnumMember(Value = "POSITIVE")]
    Positive,

    [EnumMember(Value = "NEUTRAL")]
    Neutral,

    [EnumMember(Value = "NEGATIVE")]
    Negative
}
