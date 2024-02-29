using AssemblyAI.Realtime;
using BlazorSample.Shared;

namespace BlazorSample.Server;

public class RealtimeTranscriberFactory : IRealtimeTranscriberFactory
{
    private readonly string _apiKey;

    public RealtimeTranscriberFactory(IConfiguration configuration)
    {
        _apiKey = configuration["AssemblyAI:ApiKey"]!;
        if (string.IsNullOrEmpty(_apiKey)) throw new Exception("AssemblyAI:ApiKey is not configured");
    }

    public Task<RealtimeTranscriber> CreateAsync()
    {
        return Task.FromResult(new RealtimeTranscriber()
        {
            ApiKey = _apiKey
        });
    }
}