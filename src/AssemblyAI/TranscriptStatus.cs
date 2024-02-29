using AssemblyAI.Core;

namespace AssemblyAI;

public sealed class TranscriptStatus
{
    public static readonly TranscriptStatus Error = new TranscriptStatus(Value.Error, "error");
    public static readonly TranscriptStatus Queued = new TranscriptStatus(Value.Queued, "queued");
    public static readonly TranscriptStatus Processing = new TranscriptStatus(Value.Processing, "processing");
    public static readonly TranscriptStatus Completed = new TranscriptStatus(Value.Completed, "completed");

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
            switch (value)
            {
                case "ERROR":
                    return Error;
                case "QUEUED":
                    return Queued;
                case "PROCESSING":
                    return Processing;
                case "COMPLETED":
                    return Completed;
                default:
                    throw new AssemblyAIException();
            }
        }

    public enum Value
    {
        Queued,
        Processing,
        Completed,
        Error,
        Unknown,
    }
}