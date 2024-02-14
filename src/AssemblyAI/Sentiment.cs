using System;

namespace AssemblyAI
{
    public class Sentiment
    {
        public static readonly Sentiment Neutral = new Sentiment(Value.Neutral, "NEUTRAL");

        public static readonly Sentiment Negative = new Sentiment(Value.Negative, "NEGATIVE");

        public static readonly Sentiment Positive = new Sentiment(Value.Positive, "POSITIVE");
    
    
        private readonly Value _value;
        private readonly String _raw;

        private Sentiment(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }

        public enum Value
        {
            Positive,

            Neutral,

            Negative,

            Unknown
        }
    }
}