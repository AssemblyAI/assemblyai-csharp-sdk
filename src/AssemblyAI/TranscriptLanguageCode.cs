namespace AssemblyAI;

public class TranscriptLanguageCode
{
    public static readonly TranscriptLanguageCode ES = new TranscriptLanguageCode(Value.ES, "es");
    public static readonly TranscriptLanguageCode EN_AU = new TranscriptLanguageCode(Value.EN_AU, "en_au");
    public static readonly TranscriptLanguageCode DE = new TranscriptLanguageCode(Value.DE, "de");
    public static readonly TranscriptLanguageCode PL = new TranscriptLanguageCode(Value.PL, "pl");
    public static readonly TranscriptLanguageCode IT = new TranscriptLanguageCode(Value.IT, "it");
    public static readonly TranscriptLanguageCode JA = new TranscriptLanguageCode(Value.JA, "ja");
    public static readonly TranscriptLanguageCode KO = new TranscriptLanguageCode(Value.KO, "ko");
    public static readonly TranscriptLanguageCode NL = new TranscriptLanguageCode(Value.NL, "nl");
    public static readonly TranscriptLanguageCode RU = new TranscriptLanguageCode(Value.RU, "ru");
    public static readonly TranscriptLanguageCode HI = new TranscriptLanguageCode(Value.HI, "hi");
    public static readonly TranscriptLanguageCode VI = new TranscriptLanguageCode(Value.VI, "vi");
    public static readonly TranscriptLanguageCode EN = new TranscriptLanguageCode(Value.EN, "en");
    public static readonly TranscriptLanguageCode EN_US = new TranscriptLanguageCode(Value.EN_US, "en_us");
    public static readonly TranscriptLanguageCode ZH = new TranscriptLanguageCode(Value.ZH, "zh");
    public static readonly TranscriptLanguageCode FR = new TranscriptLanguageCode(Value.FR, "fr");
    public static readonly TranscriptLanguageCode PT = new TranscriptLanguageCode(Value.PT, "pt");
    public static readonly TranscriptLanguageCode TR = new TranscriptLanguageCode(Value.TR, "tr");
    public static readonly TranscriptLanguageCode EN_UK = new TranscriptLanguageCode(Value.EN_UK, "en_uk");
    public static readonly TranscriptLanguageCode FI = new TranscriptLanguageCode(Value.FI, "fi");
    public static readonly TranscriptLanguageCode UK = new TranscriptLanguageCode(Value.UK, "uk");
    
    private readonly Value _value;
    private readonly String _raw;

    private TranscriptLanguageCode(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
    public TranscriptLanguageCode(string raw) {
        this._raw = raw;
    }
    
    public enum Value {
        EN,
        EN_AU,
        EN_UK,
        EN_US,
        ES,
        FR,
        DE,
        IT,
        PT,
        NL,
        HI,
        JA,
        ZH,
        FI,
        KO,
        PL,
        RU,
        TR,
        UK,
        VI,
    }
}