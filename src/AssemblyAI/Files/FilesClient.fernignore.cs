using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssemblyAI.Core;

namespace AssemblyAI.Files
{
    public partial class FilesClient
    {
        public async Task<UploadedFile> Upload(Stream audioStream, RequestOptions requestOptions = null)
        {
            var url = new URLBuilder(_clientWrapper.BaseUrl)
                .AddPathSegment("v2/upload")
                .build();
            HttpResponseMessage response;
            using (var content = new StreamContent(audioStream))
            {
                response = await _clientWrapper.HttpClient.PostAsync(
                    url,
                    content
                );
            }

            using (response)
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                    };
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonSerializer.Deserialize<UploadedFile>(contentStream);
                }
            }
        }
    }
}