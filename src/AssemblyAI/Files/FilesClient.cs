using System.Text.Json;
using AssemblyAI.Core;

namespace AssemblyAI
{
    public partial class FilesClient
    {
        private readonly ClientWrapper _clientWrapper;

        public FilesClient(ClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        public async Task<UploadedFile> Upload(byte[] request, RequestOptions? requestOptions = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/upload")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new ByteArrayContent(request));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<UploadedFile>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
    }    
}
