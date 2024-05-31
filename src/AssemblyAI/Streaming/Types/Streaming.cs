using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<Streaming>))]
public enum Streaming
{
    [EnumMember(Value = "pcm_s16le")]
    PcmS16le,

    [EnumMember(Value = "pcm_mulaw")]
    PcmMulaw
}
