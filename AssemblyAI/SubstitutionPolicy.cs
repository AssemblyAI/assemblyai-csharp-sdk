namespace AssemblyAI;

public class SubstitutionPolicy
{
    public static readonly SubstitutionPolicy EntityType = new SubstitutionPolicy(Value.EntityType, "entity_type");

    public static readonly SubstitutionPolicy Hash = new SubstitutionPolicy(Value.Hash, "hash");
    
        
    private readonly Value _value;
    private readonly String _raw;

    private SubstitutionPolicy(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
    public enum Value
    {
        EntityType,
        Hash,
        Unknown
    }
}