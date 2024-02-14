namespace AssemblyAI
{
    public class SummaryModel
    {
        public static readonly SummaryModel Informative = new SummaryModel(Value.Informative, "informative");

        public static readonly SummaryModel Catchy = new SummaryModel(Value.Catchy, "catchy");

        public static readonly SummaryModel Conversational = new SummaryModel(Value.Conversational, "conversational");

        private readonly Value _value;
        private readonly string _raw;

        private SummaryModel(Value value, string raw)
    {
        this._value = value;
        this._raw = raw;
    }
    
        enum Value
        {
            Informative,
            Conversational,
            Catchy,
            Unknown
        }
    }
}