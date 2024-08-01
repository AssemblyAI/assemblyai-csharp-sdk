using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<SubstitutionPolicy>))]
public enum SubstitutionPolicy
{
    [EnumMember(Value = "entity_name")]
    EntityName,

    [EnumMember(Value = "hash")]
    Hash
}
