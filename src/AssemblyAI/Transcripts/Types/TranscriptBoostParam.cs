using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<TranscriptBoostParam>))]
public enum TranscriptBoostParam
{
    [EnumMember(Value = "low")]
    Low,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "high")]
    High,
}
