using System.Net.Http;
using System.Text.Json;
using System.Threading;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

public partial class RealtimeClient
{
    private RawClient _client;

    internal RealtimeClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Create a temporary authentication token for Streaming Speech-to-Text
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Realtime.CreateTemporaryTokenAsync(
    ///     new CreateRealtimeTemporaryTokenParams { ExpiresIn = 480 }
    /// );
    /// </code>
    /// </example>
    public async Task<RealtimeTemporaryTokenResponse> CreateTemporaryTokenAsync(
        CreateRealtimeTemporaryTokenParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "v2/realtime/token",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<RealtimeTemporaryTokenResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }
}
