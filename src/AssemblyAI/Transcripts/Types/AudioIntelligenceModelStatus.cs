using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<AudioIntelligenceModelStatus>))]
public enum AudioIntelligenceModelStatus
{
    [EnumMember(Value = "success")]
    Success,

    [EnumMember(Value = "unavailable")]
    Unavailable,
}
