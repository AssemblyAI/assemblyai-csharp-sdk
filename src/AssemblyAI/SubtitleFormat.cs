namespace AssemblyAI;

public class SubtitleFormat
{
    public static readonly SubtitleFormat VTT = new SubtitleFormat(Value.VTT, "vtt");

    public static readonly SubtitleFormat SRT = new SubtitleFormat(Value.SRT, "srt");

    private readonly Value _value;
    private readonly String _raw;

    private SubtitleFormat(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
    public enum Value
    {
        SRT,
        VTT,
        UNKNOWN
    }

    public override string ToString()
    {
        return _raw;
    }
}