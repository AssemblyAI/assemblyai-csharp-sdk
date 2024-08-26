using System.Net.Http;
using System.Text.Json;
using System.Threading;
using AssemblyAI;
using AssemblyAI.Core;
using OneOf;

#nullable enable

namespace AssemblyAI.Lemur;

public partial class LemurClient
{
    private RawClient _client;

    internal LemurClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use the LeMUR task endpoint to input your own LLM prompt.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.TaskAsync(
    ///     new LemurTaskParams
    ///     {
    ///         TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
    ///         Context = "This is an interview about wildfires.",
    ///         FinalModel = LemurModel.Default,
    ///         MaxOutputSize = 3000,
    ///         Temperature = 0f,
    ///         Prompt = "List all the locations affected by wildfires.",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<LemurTaskResponse> TaskAsync(
        LemurTaskParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/task",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<LemurTaskResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Custom Summary allows you to distill a piece of audio into a few impactful sentences.
    /// You can give the model context to obtain more targeted results while outputting the results in a variety of formats described in human language.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.SummaryAsync(
    ///     new LemurSummaryParams
    ///     {
    ///         TranscriptIds = new List<string>() { "47b95ba5-8889-44d8-bc80-5de38306e582" },
    ///         Context = "This is an interview about wildfires.",
    ///         FinalModel = LemurModel.Default,
    ///         MaxOutputSize = 3000,
    ///         Temperature = 0f,
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<LemurSummaryResponse> SummaryAsync(
        LemurSummaryParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/summary",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<LemurSummaryResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Question & Answer allows you to ask free-form questions about a single transcript or a group of transcripts.
    /// The questions can be any whose answers you find useful, such as judging whether a caller is likely to become a customer or whether all items on a meeting's agenda were covered.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.QuestionAnswerAsync(
    ///     new LemurQuestionAnswerParams
    ///     {
    ///         TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
    ///         Context = "This is an interview about wildfires.",
    ///         FinalModel = LemurModel.Default,
    ///         MaxOutputSize = 3000,
    ///         Temperature = 0f,
    ///         Questions = new List<LemurQuestion>()
    ///         {
    ///             new LemurQuestion
    ///             {
    ///                 Question = "Where are there wildfires?",
    ///                 AnswerFormat = "List of countries in ISO 3166-1 alpha-2 format",
    ///                 AnswerOptions = new List<string>() { "US", "CA" },
    ///             },
    ///             new LemurQuestion
    ///             {
    ///                 Question = "Is global warming affecting wildfires?",
    ///                 AnswerOptions = new List<string>() { "yes", "no" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<LemurQuestionAnswerResponse> QuestionAnswerAsync(
        LemurQuestionAnswerParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/question-answer",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<LemurQuestionAnswerResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Use LeMUR to generate a list of action items from a transcript
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.ActionItemsAsync(
    ///     new LemurActionItemsParams
    ///     {
    ///         TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
    ///         Context = "This is an interview about wildfires.",
    ///         FinalModel = LemurModel.Default,
    ///         MaxOutputSize = 3000,
    ///         Temperature = 0f,
    ///         AnswerFormat = "Bullet Points",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<LemurActionItemsResponse> ActionItemsAsync(
        LemurActionItemsParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "lemur/v3/generate/action-items",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<LemurActionItemsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Retrieve a LeMUR response that was previously generated.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.GetResponseAsync("request_id");
    /// </code>
    /// </example>
    public async Task<OneOf<LemurStringResponse, LemurQuestionAnswerResponse>> GetResponseAsync(
        string requestId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"lemur/v3/{requestId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<
                    OneOf<LemurStringResponse, LemurQuestionAnswerResponse>
                >(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Delete the data for a previously submitted LeMUR request.
    /// The LLM response data, as well as any context provided in the original request will be removed.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Lemur.PurgeRequestDataAsync("request_id");
    /// </code>
    /// </example>
    public async Task<PurgeLemurRequestDataResponse> PurgeRequestDataAsync(
        string requestId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"lemur/v3/{requestId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<PurgeLemurRequestDataResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new AssemblyAIException("Failed to deserialize response", e);
            }
        }

        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }
}
