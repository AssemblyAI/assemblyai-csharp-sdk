using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<RedactPiiAudioQuality>))]
public enum RedactPiiAudioQuality
{
    [EnumMember(Value = "mp3")]
    Mp3,

    [EnumMember(Value = "wav")]
    Wav,
}
