using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum RedactPiiAudioQuality
{
    [EnumMember(Value = "mp3")]
    Mp3,

    [EnumMember(Value = "wav")]
    Wav
}
