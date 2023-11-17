using AssemblyAI.Core;

namespace AssemblyAI;

public class AudioIntelligenceModelStatus
{
    public static readonly AudioIntelligenceModelStatus SUCCESS = 
        new AudioIntelligenceModelStatus("success", Value.SUCCESS);
    public static readonly AudioIntelligenceModelStatus UNAVAILABLE = 
        new AudioIntelligenceModelStatus("unavailable", Value.UNAVAILABLE);

    private readonly string _raw;
    private readonly Value _value;

    private AudioIntelligenceModelStatus(string raw, Value value)
    {
        this._raw = raw;
        this._value = value;
    }
    
            
    public static AudioIntelligenceModelStatus Of(string value)
    {
        return value switch
        {
            "success" => SUCCESS,
            "unavailable" => UNAVAILABLE,
            _ => throw new AssemblyAIError()
        };
    }

    public enum Value
    {
        SUCCESS,
        UNAVAILABLE
    }
}