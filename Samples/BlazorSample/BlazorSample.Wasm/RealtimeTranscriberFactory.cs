using System.Net.Http.Json;
using AssemblyAI;
using AssemblyAI.Realtime;
using BlazorSample.Shared;

namespace BlazorSample.Wasm;

public class RealtimeTranscriberFactory(HttpClient httpClient) : IRealtimeTranscriberFactory
{
    public async Task<RealtimeTranscriber> CreateAsync()
    {
        var tokenResponse = await httpClient.PostAsync("api/realtime/token", null);
        tokenResponse.EnsureSuccessStatusCode();
        var tokenResponseObject = await tokenResponse.Content.ReadFromJsonAsync<RealtimeTemporaryTokenResponse>();
        return new RealtimeTranscriber
        {
            Token = tokenResponseObject!.Token
        };
    }
}