using System.Text.Json;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

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
    public async Task<LemurTaskResponse> TaskAsync(LemurTaskParams request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/lemur/v3/generate/task",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<LemurTaskResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Custom Summary allows you to distill a piece of audio into a few impactful sentences.
    /// You can give the model context to obtain more targeted results while outputting the results in a variety of formats described in human language.
    /// </summary>
    public async Task<LemurSummaryResponse> SummaryAsync(LemurSummaryParams request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/lemur/v3/generate/summary",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<LemurSummaryResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Question & Answer allows you to ask free-form questions about a single transcript or a group of transcripts.
    /// The questions can be any whose answers you find useful, such as judging whether a caller is likely to become a customer or whether all items on a meeting's agenda were covered.
    /// </summary>
    public async Task<LemurQuestionAnswerResponse> QuestionAnswerAsync(
        LemurQuestionAnswerParams request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/lemur/v3/generate/question-answer",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<LemurQuestionAnswerResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Use LeMUR to generate a list of action items from a transcript
    /// </summary>
    public async Task<LemurActionItemsResponse> ActionItemsAsync(LemurActionItemsParams request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/lemur/v3/generate/action-items",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<LemurActionItemsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Delete the data for a previously submitted LeMUR request.
    /// The LLM response data, as well as any context provided in the original request will be removed.
    /// </summary>
    public async Task<PurgeLemurRequestDataResponse> PurgeRequestDataAsync(string requestId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest { Method = HttpMethod.Delete, Path = $"/lemur/v3/{requestId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<PurgeLemurRequestDataResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
