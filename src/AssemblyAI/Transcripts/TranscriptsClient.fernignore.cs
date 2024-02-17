using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssemblyAI.Core;

namespace AssemblyAI.Transcripts
{
    public partial class TranscriptsClient
    {
        /**
        * Create a transcript from an audio or video file that is accessible via a URL.
        */
        public async Task<Transcript> Create(
            string audioUrl,
            CreateTranscriptOptionalParameters request,
            RequestOptions options = null
        )
        {
            var paramsJson = JsonSerializer.SerializeToNode(request);
            paramsJson.AsObject().Add("audio_url", audioUrl);

            var url = new URLBuilder(_clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .build();
            var response = await _clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(paramsJson.ToJsonString()));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Transcript>(await response.Content.ReadAsStringAsync());
            }

            throw new ApiException
            {
                StatusCode = (int)response.StatusCode,
            };
        }
    }
}