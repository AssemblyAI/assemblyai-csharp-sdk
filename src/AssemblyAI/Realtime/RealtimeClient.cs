using System.Text.Json;
using AssemblyAI.Core;

namespace AssemblyAI
{
    public partial class RealtimeClient
    {
        private readonly ClientWrapper _clientWrapper;

        public RealtimeClient(ClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }
        
        /**
         * Retrieve a IEnumerable of transcripts you have created.
         */
        public async Task<TranscriptIEnumerable> CreateTemporaryToken(
            CreateRealtimeTemporaryTokenParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/realtime/token")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<TranscriptIEnumerable>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
    }    
}
