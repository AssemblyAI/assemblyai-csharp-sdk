using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;
using AssemblyAI.Realtime;

#nullable enable

namespace AssemblyAI.Realtime;

public partial class RealtimeClient
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
        CreateRealtimeTemporaryTokenParams request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "v2/realtime/token",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<RealtimeTemporaryTokenResponse>(responseBody)!;
    }
}
