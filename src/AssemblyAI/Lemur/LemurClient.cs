using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;
using AssemblyAI.Lemur;
using OneOf;

#nullable enable

namespace AssemblyAI.Lemur;

public class LemurClient
{
    private RawClient _client;

    public LemurClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use the LeMUR task endpoint to input your own LLM prompt.
    /// </summary>
    public async Task<LemurTaskResponse> TaskAsync(
        LemurTaskParams request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/task",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<LemurTaskResponse>(responseBody)!;
    }

    /// <summary>
    /// Custom Summary allows you to distill a piece of audio into a few impactful sentences.
    /// You can give the model context to obtain more targeted results while outputting the results in a variety of formats described in human language.
    /// </summary>
    public async Task<LemurSummaryResponse> SummaryAsync(
        LemurSummaryParams request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/summary",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<LemurSummaryResponse>(responseBody)!;
    }

    /// <summary>
    /// Question & Answer allows you to ask free-form questions about a single transcript or a group of transcripts.
    /// The questions can be any whose answers you find useful, such as judging whether a caller is likely to become a customer or whether all items on a meeting's agenda were covered.
    /// </summary>
    public async Task<LemurQuestionAnswerResponse> QuestionAnswerAsync(
        LemurQuestionAnswerParams request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/question-answer",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<LemurQuestionAnswerResponse>(responseBody)!;
    }

    /// <summary>
    /// Use LeMUR to generate a list of action items from a transcript
    /// </summary>
    public async Task<LemurActionItemsResponse> ActionItemsAsync(
        LemurActionItemsParams request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/action-items",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<LemurActionItemsResponse>(responseBody)!;
    }

    /// <summary>
    /// Retrieve a LeMUR response that was previously generated.
    /// </summary>
    public async Task<OneOf<LemurStringResponse, LemurQuestionAnswerResponse>> GetResponseAsync(
        string requestId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"lemur/v3/{requestId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<OneOf<LemurStringResponse, LemurQuestionAnswerResponse>>(responseBody)!;
    }

    /// <summary>
    /// Delete the data for a previously submitted LeMUR request.
    /// The LLM response data, as well as any context provided in the original request will be removed.
    /// </summary>
    public async Task<PurgeLemurRequestDataResponse> PurgeRequestDataAsync(
        string requestId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"lemur/v3/{requestId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        return JsonUtils.Deserialize<PurgeLemurRequestDataResponse>(responseBody)!;
    }
}
