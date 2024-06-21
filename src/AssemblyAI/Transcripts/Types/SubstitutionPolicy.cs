using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<SubstitutionPolicy>))]
public enum SubstitutionPolicy
{
    [EnumMember(Value = "entity_name")]
    EntityName,

    [EnumMember(Value = "hash")]
    Hash
}
