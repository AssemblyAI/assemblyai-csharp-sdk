using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum RedactPiiAudioQuality
{
    [EnumMember(Value = "mp3")]
    Mp3,

    [EnumMember(Value = "wav")]
    Wav
}
