using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<SummaryModel>))]
public enum SummaryModel
{
    [EnumMember(Value = "informative")]
    Informative,

    [EnumMember(Value = "conversational")]
    Conversational,

    [EnumMember(Value = "catchy")]
    Catchy,
}
