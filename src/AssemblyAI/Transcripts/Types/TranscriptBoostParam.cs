using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<TranscriptBoostParam>))]
public enum TranscriptBoostParam
{
    [EnumMember(Value = "low")]
    Low,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "high")]
    High
}
