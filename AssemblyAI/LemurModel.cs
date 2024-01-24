namespace AssemblyAI;

public class LemurModel
{
    public static LemurModel BASIC { get; } = new LemurModel(Value.BASIC, "basic");

    public static LemurModel DEFAULT { get; } = new LemurModel(Value.DEFAULT, "default");
    
    
    private readonly Value _value;
    private readonly String _raw;

    private LemurModel(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }

    public enum Value
    {
        DEFAULT,
        BASIC,
        UNKNOWN
    }
}