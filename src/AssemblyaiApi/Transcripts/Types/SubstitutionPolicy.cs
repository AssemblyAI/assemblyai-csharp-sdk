using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum SubstitutionPolicy
{
    [EnumMember(Value = "entity_name")]
    EntityName,

    [EnumMember(Value = "hash")]
    Hash
}
