using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum Streaming
{
    [EnumMember(Value = "pcm_s16le")]
    PcmS16le,

    [EnumMember(Value = "pcm_mulaw")]
    PcmMulaw
}
