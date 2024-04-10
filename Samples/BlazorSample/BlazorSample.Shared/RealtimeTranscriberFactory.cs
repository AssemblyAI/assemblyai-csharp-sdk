using AssemblyAI.Realtime;

namespace BlazorSample.Shared;

public interface IRealtimeTranscriberFactory
{
    public Task<RealtimeTranscriber> CreateAsync();
}