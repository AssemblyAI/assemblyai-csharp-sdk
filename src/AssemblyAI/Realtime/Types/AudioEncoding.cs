using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Realtime;

#nullable enable

namespace AssemblyAI.Realtime;

[JsonConverter(typeof(StringEnumSerializer<AudioEncoding>))]
public enum AudioEncoding
{
    [EnumMember(Value = "pcm_s16le")]
    PcmS16le,

    [EnumMember(Value = "pcm_mulaw")]
    PcmMulaw
}
