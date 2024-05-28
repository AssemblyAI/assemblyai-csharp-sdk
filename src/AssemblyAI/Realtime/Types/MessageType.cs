using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

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
