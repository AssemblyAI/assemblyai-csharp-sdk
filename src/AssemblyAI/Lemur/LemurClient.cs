using System.Text.Json;
using AssemblyAI.Core;

namespace AssemblyAI
{
    public partial class LemurClient
    {
        private readonly ClientWrapper _clientWrapper;

        public LemurClient(ClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        public async Task<LemurSummaryResponse> Summary(LemurSummaryParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("lemur/v3/generate/summary")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<LemurSummaryResponse>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        public async Task<LemurQuestionAnswerResponse> QuestionAnswer(
            LemurSummaryParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("lemur/v3/generate/question-answer")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<LemurQuestionAnswerResponse>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }

        public async Task<LemurActionItemsResponse> ActionItems(
            LemurBaseParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("lemur/v3/generate/action-items")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<LemurActionItemsResponse>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        public async Task<LemurTaskResponse> Task(
            LemurTaskParameters request, RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("v2/realtime/token")
                .build();
            var response = await this._clientWrapper.HttpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(request)));
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<LemurTaskResponse>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
        public async Task<PurgeLemurRequestDataResponse> Task(
            string requestId,  RequestOptions? options = null)
        {
            var url = new URLBuilder(this._clientWrapper.BaseUrl)
                .AddPathSegment("lemur/v3")
                .AddPathSegment(requestId)
                .build();
            var response = await this._clientWrapper.HttpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<PurgeLemurRequestDataResponse>(await response.Content.ReadAsStringAsync());
            }
            throw new ApiException
            {
                StatusCode = (int) response.StatusCode,
            };
        }
        
    }    
}
