using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum SubstitutionPolicy
{
    [EnumMember(Value = "entity_name")]
    EntityName,

    [EnumMember(Value = "hash")]
    Hash
}
