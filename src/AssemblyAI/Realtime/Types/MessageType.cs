using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

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
