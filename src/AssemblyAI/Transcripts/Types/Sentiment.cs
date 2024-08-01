using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

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
