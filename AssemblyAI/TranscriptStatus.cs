using AssemblyAI.Core;

namespace AssemblyAI
{
    public sealed class TranscriptStatus
    {
        public static readonly TranscriptStatus ERROR = new TranscriptStatus(Value.ERROR, "error");
        public static readonly TranscriptStatus QUEUED = new TranscriptStatus(Value.QUEUED, "queued");
        public static readonly TranscriptStatus PROCESSING = new TranscriptStatus(Value.PROCESSING, "processing");
        public static readonly TranscriptStatus COMPLETED = new TranscriptStatus(Value.COMPLETED, "completed");

        private readonly Value value;
        private readonly string raw;

        private TranscriptStatus(Value value, string raw)
        {
            this.value = value;
            this.raw = raw;
        }
        
        /**
         * Returns the underlying enum value for TranscriptStatus.
         * ```
         * switch (status._Value()) {
         *  case TranscriptStatus.Value.Error:
         *    return;
         *  case TranscriptStatus.Value.QUEUED:
         *    return;
         * }
         * ```
         */
        public Value _Value()
        {
            return value;
        }
        
        public static TranscriptStatus Of(string value)
        {
            return value switch
            {
                "ERROR" => ERROR,
                "QUEUED" => QUEUED,
                "PROCESSING" => PROCESSING,
                "COMPLETED" => COMPLETED,
                _ => throw new AssemblyAIError()
            };
        }

        public enum Value
        {
            QUEUED,
            PROCESSING,
            COMPLETED,
            ERROR,
            UNKNOWN,
        }
    }
}