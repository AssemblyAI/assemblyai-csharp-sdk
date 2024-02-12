namespace AssemblyAI;

public class LemurModel
{
    public static LemurModel Basic { get; } = new LemurModel(Value.Basic, "basic");

    public static LemurModel Default { get; } = new LemurModel(Value.Default, "default");
    
    
    private readonly Value _value;
    private readonly String _raw;

    private LemurModel(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }

    public enum Value
    {
        Default,
        Basic,
        Unknown
    }
}