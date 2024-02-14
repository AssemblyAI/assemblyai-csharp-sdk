namespace AssemblyAI
{
    public class SummaryType {
        public static readonly SummaryType Headline = new SummaryType(Value.Headline, "headline");

        public static readonly SummaryType Gist = new SummaryType(Value.Gist, "gist");

        public static readonly SummaryType BulletsVerbose = new SummaryType(Value.BulletsVerbose, "bullets_verbose");

        public static readonly SummaryType Bullets = new SummaryType(Value.Bullets, "bullets");

        public static readonly SummaryType Paragraph = new SummaryType(Value.Paragraph, "paragraph");

        private readonly Value _value;
        private readonly string _raw;

        private SummaryType(Value value, string raw)
    {
        this._value = value;
        this._raw = raw;
    }

        public enum Value
        {
            Bullets,
            BulletsVerbose,
            Gist,
            Headline,
            Paragraph,
            Unknown
        }
    }
}