using System.Text.Json;
using AssemblyAI.Core;

namespace AssemblyAI
{
    public partial class TranscriptsClient
    {
        private readonly ClientWrapper _clientWrapper;

        public TranscriptsClient(ClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }
        
        /**
        * Retrieve a IEnumerable of transcripts you have created.
        */
        public async Task<TranscriptIEnumerable> IEnumerable()
        {
            return await IEnumerable(new TranscriptIEnumerableRequest{});
        }

        /**
        * Retrieve a IEnumerable of transcripts you have created.
        */
        public async Task<TranscriptIEnumerable> IEnumerable(TranscriptIEnumerableRequest request, RequestOptions? options = null)
        {
            var urlBuilder = new URLBuilder(this._clientWrapper.BaseUrl);
            urlBuilder.AddPathSegment("v2/transcript");
            if (request.Limit.HasValue)
            {
                var a = request.Limit;
                urlBuilder.AddQueryParameter("limit", request.Limit.Value.ToString());
            }
            if (request.CreatedOn != null)
            {
                urlBuilder.AddQueryParameter("after_id", request.CreatedOn);
            }
            if (request.BeforeId != null)
            {
                urlBuilder.AddQueryParameter("after_id", request.BeforeId);
            }
            if (request.AfterId != null)
            {
                urlBuilder.AddQueryParameter("after_id", request.AfterId);
            }
            if (request.ThrottledOnly.HasValue)
            {
                urlBuilder.AddQueryParameter("after_id", request.ThrottledOnly.Value.ToString());
            }
            HttpResponseMessage response = 
                await this._clientWrapper.HttpClient.GetAsync(urlBuilder.build());
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<TranscriptIEnumerable>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }

        /**
        * Create a transcript from an audio or video file that is accessible via a URL.
        */
        public async Task<Transcript> Create(CreateTranscriptParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url, 
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Transcript>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        /**
         * Get the transcript resource. The transcript is ready when the &quot;status&quot; is &quot;completed&quot;.
         */
        public async Task<Transcript> Get(string transcriptId, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .AddPathSegment(transcriptId)
                .build();
            var response = await this._clientWrapper.HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Transcript>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        /**
        * Delete the transcript
        */
        public async Task<Transcript> Delete(string transcriptId, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .AddPathSegment(transcriptId)
                .build();
            var response = await this._clientWrapper.HttpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Transcript>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        /**
         * Export your transcript in SRT or VTT format, to be plugged into a video player for subtitles and closed captions.
         */
        public async Task<string> GetSubtitles(string transcriptId, SubtitleFormat subtitleFormat)
        {
            return await GetSubtitles(transcriptId, subtitleFormat, new TranscriptGetSubtitlesRequest() { });
        }
        
        /**
         * Export your transcript in SRT or VTT format, to be plugged into a video player for subtitles and closed captions.
         */
        public async Task<string> GetSubtitles(
            string transcriptId, 
            SubtitleFormat subtitleFormat, 
            TranscriptGetSubtitlesRequest request, 
            RequestOptions? options = null)
        {
            var urlBuilder = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .AddPathSegment(transcriptId)
                .AddPathSegment(subtitleFormat.ToString());
            if (request.CharsPerCaption.HasValue)
            {
                urlBuilder.AddQueryParameter("chars_per_caption", request.CharsPerCaption.Value.ToString());
            }
            var response = await this._clientWrapper.HttpClient.GetAsync(urlBuilder.build());
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        /**
        * Get the transcript split by sentences. The API will attempt to semantically segment the transcript into sentences to create more reader-friendly transcripts.
        */
        public async Task<string> GetSentences(string transcriptId, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/transcript")
                .AddPathSegment(transcriptId)
                .AddPathSegment("sentences")
                .build();
            var response = await this._clientWrapper.HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
    }   
}