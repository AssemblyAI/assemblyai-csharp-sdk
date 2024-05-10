using System.Text.Json;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class RealtimeClient
{
    private RawClient _client;

    public RealtimeClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Create a temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    public async Task<RealtimeTemporaryTokenResponse> CreateTemporaryTokenAsync(
        CreateRealtimeTemporaryTokenParams request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/v2/realtime/token",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<RealtimeTemporaryTokenResponse>(responseBody);
        }
        throw new Exception();
    }
}
