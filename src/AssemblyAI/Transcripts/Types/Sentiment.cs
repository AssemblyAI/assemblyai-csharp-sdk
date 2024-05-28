using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum Sentiment
{
    [EnumMember(Value = "POSITIVE")]
    Positive,

    [EnumMember(Value = "NEUTRAL")]
    Neutral,

    [EnumMember(Value = "NEGATIVE")]
    Negative
}
