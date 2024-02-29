namespace AssemblyAI;

public class TranscriptBoostParam
{
    public static readonly TranscriptBoostParam High = 
        new TranscriptBoostParam(Value.High, "high");
    public static readonly TranscriptBoostParam Low = 
        new TranscriptBoostParam(Value.Low, "low");
    public static readonly TranscriptBoostParam Default = 
        new TranscriptBoostParam(Value.Default, "default");
    
    private readonly Value _value;
    private readonly String _raw;

    private TranscriptBoostParam(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
    public enum Value {
        Low,
        Default,
        High,
        UNKNOWN
    }
}