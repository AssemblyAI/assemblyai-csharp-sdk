using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Files;

public partial class FilesClient
{
    private RawClient _client;

    internal FilesClient(RawClient client)
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
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            JsonUtils.Deserialize<object>(responseBody)
        );
    }
}
