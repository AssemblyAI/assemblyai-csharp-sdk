using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<SpeechModel>))]
public enum SpeechModel
{
    [EnumMember(Value = "best")]
    Best,

    [EnumMember(Value = "nano")]
    Nano,
}
