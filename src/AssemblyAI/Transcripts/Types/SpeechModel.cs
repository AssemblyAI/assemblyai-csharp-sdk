using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<SpeechModel>))]
public enum SpeechModel
{
    [EnumMember(Value = "best")]
    Best,

    [EnumMember(Value = "nano")]
    Nano
}
