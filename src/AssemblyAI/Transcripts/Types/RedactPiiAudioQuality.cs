using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<RedactPiiAudioQuality>))]
public enum RedactPiiAudioQuality
{
    [EnumMember(Value = "mp3")]
    Mp3,

    [EnumMember(Value = "wav")]
    Wav
}
