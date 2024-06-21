using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<RedactPiiAudioQuality>))]
public enum RedactPiiAudioQuality
{
    [EnumMember(Value = "mp3")]
    Mp3,

    [EnumMember(Value = "wav")]
    Wav
}
