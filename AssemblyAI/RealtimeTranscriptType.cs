namespace AssemblyAI;

public class RealtimeTranscriptType
{
    public static readonly RealtimeTranscriptType FinalTranscript =
        new RealtimeTranscriptType(Value.FinalTranscript, "FinalTranscript");

    public static readonly RealtimeTranscriptType PartialTranscript =
        new RealtimeTranscriptType(Value.PartialTranscript, "PartialTranscript");
    
    
    private readonly Value _value;
    private readonly String _raw;

    private RealtimeTranscriptType(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
    public enum Value
    {
        PartialTranscript,

        FinalTranscript,

        Unknown
    }
}