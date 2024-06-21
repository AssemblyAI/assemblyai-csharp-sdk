using System.Text.Json;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

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
    public async Task<UploadedFile> UploadAsync(Stream request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.StreamApiRequest
            {
                Method = HttpMethod.Post,
                Path = "v2/upload",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UploadedFile>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
