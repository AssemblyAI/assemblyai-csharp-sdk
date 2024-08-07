using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;
using AssemblyAI.Files;

#nullable enable

namespace AssemblyAI.Files;

public partial class FilesClient
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
        return JsonUtils.Deserialize<UploadedFile>(responseBody)!;
    }
}
