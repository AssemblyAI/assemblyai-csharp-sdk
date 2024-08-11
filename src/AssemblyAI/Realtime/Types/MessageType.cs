using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

[JsonConverter(typeof(StringEnumSerializer<MessageType>))]
public enum MessageType
{
    [EnumMember(Value = "SessionBegins")]
    SessionBegins,

    [EnumMember(Value = "PartialTranscript")]
    PartialTranscript,

    [EnumMember(Value = "FinalTranscript")]
    FinalTranscript,

    [EnumMember(Value = "SessionInformation")]
    SessionInformation,

    [EnumMember(Value = "SessionTerminated")]
    SessionTerminated
}
