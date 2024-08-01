using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;
using AssemblyAI.Files;

#nullable enable

namespace AssemblyAI.Files;

public class FilesClient
{
    private RawClient _client;

    public FilesClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Upload a media file to AssemblyAI's servers.
    /// </summary>
    public async Task<UploadedFile> UploadAsync(Stream request, RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.StreamApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "v2/upload",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UploadedFile>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIClientException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<Error>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<Error>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<Error>(responseBody));
                case 429:
                    throw new TooManyRequestsError(JsonUtils.Deserialize<Error>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<Error>(responseBody));
                case 503:
                    throw new ServiceUnavailableError(JsonUtils.Deserialize<object>(responseBody));
                case 504:
                    throw new GatewayTimeoutError(JsonUtils.Deserialize<object>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new AssemblyAIClientApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            JsonUtils.Deserialize<object>(responseBody)
        );
    }
}
