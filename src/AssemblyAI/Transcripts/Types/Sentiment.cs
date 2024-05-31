using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<Sentiment>))]
public enum Sentiment
{
    [EnumMember(Value = "POSITIVE")]
    Positive,

    [EnumMember(Value = "NEUTRAL")]
    Neutral,

    [EnumMember(Value = "NEGATIVE")]
    Negative
}
