using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

[JsonConverter(typeof(StringEnumSerializer<Realtime>))]
public enum Realtime
{
    [EnumMember(Value = "pcm_s16le")]
    PcmS16le,

    [EnumMember(Value = "pcm_mulaw")]
    PcmMulaw
}
