using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(EnumSerializer<SubstitutionPolicy>))]
public enum SubstitutionPolicy
{
    [EnumMember(Value = "entity_name")]
    EntityName,

    [EnumMember(Value = "hash")]
    Hash,
}
