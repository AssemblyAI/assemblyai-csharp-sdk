using AssemblyAI.Core;

namespace AssemblyAI
{
    public class AudioIntelligenceModelStatus
    {
        public static readonly AudioIntelligenceModelStatus Success = 
            new AudioIntelligenceModelStatus("success", Value.Success);
        public static readonly AudioIntelligenceModelStatus Unavailable = 
            new AudioIntelligenceModelStatus("unavailable", Value.Unavailable);

        private readonly string _raw;
        private readonly Value _value;

        private AudioIntelligenceModelStatus(string raw, Value value)
    {
        this._raw = raw;
        this._value = value;
    }
    
            
        public static AudioIntelligenceModelStatus Of(string value)
        {
            switch (value)
            {
                case "success":
                    return Success;
                case "unavailable":
                    return Unavailable;
                default:
                    throw new AssemblyAIException();
            }
        }

        public enum Value
        {
            Success,
            Unavailable
        }
    }
}