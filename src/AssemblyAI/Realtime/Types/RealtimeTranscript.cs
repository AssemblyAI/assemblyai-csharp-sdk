using OneOf;

// ReSharper disable once CheckNamespace
namespace AssemblyAI.Realtime;

public class RealtimeTranscript : OneOfBase<PartialTranscript, FinalTranscript>
{
    private RealtimeTranscript(OneOf<PartialTranscript, FinalTranscript> transcript) : base(transcript)
    {
    }

    public static implicit operator RealtimeTranscript(PartialTranscript _) => new(_);
    public static implicit operator RealtimeTranscript(FinalTranscript _) => new(_);
    
    public bool IsPartialTranscript => IsT0;
    public bool IsFinalTranscript => IsT1;
    public PartialTranscript AsPartialTranscript => AsT0;
    public FinalTranscript AsFinalTranscript => AsT1;
}