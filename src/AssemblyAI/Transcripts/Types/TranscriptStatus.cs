using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<TranscriptStatus>))]
public enum TranscriptStatus
{
    [EnumMember(Value = "queued")]
    Queued,

    [EnumMember(Value = "processing")]
    Processing,

    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "error")]
    Error
}
