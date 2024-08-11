using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<TranscriptReadyStatus>))]
public enum TranscriptReadyStatus
{
    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "error")]
    Error
}
