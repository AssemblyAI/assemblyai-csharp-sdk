using System.Net.Http;
using System.Text.Json;
using System.Threading;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

public partial class TranscriptsClient
{
    private RawClient _client;

    internal TranscriptsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Retrieve a list of transcripts you created.
    /// Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.ListAsync(new ListTranscriptParams());
    /// </code>
    /// </example>
    public async Task<TranscriptList> ListAsync(
        ListTranscriptParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Status != null)
        {
            _query["status"] = request.Status.Value.Stringify();
        }
        if (request.CreatedOn != null)
        {
            _query["created_on"] = request.CreatedOn;
        }
        if (request.BeforeId != null)
        {
            _query["before_id"] = request.BeforeId;
        }
        if (request.AfterId != null)
        {
            _query["after_id"] = request.AfterId;
        }
        if (request.ThrottledOnly != null)
        {
            _query["throttled_only"] = request.ThrottledOnly.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "v2/transcript",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<TranscriptList>(responseBody)!;
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
    /// Create a transcript from a media file that is accessible via a URL.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.SubmitAsync(
    ///     new TranscriptParams
    ///     {
    ///         LanguageCode = TranscriptLanguageCode.EnUs,
    ///         LanguageDetection = true,
    ///         LanguageConfidenceThreshold = 0.7f,
    ///         Punctuate = true,
    ///         FormatText = true,
    ///         Disfluencies = false,
    ///         Multichannel = true,
    ///         DualChannel = false,
    ///         WebhookUrl = "https://your-webhook-url/path",
    ///         WebhookAuthHeaderName = "webhook-secret",
    ///         WebhookAuthHeaderValue = "webhook-secret-value",
    ///         AutoHighlights = true,
    ///         AudioStartFrom = 10,
    ///         AudioEndAt = 280,
    ///         WordBoost = new List&lt;string&gt;() { "aws", "azure", "google cloud" },
    ///         BoostParam = TranscriptBoostParam.High,
    ///         FilterProfanity = true,
    ///         RedactPii = true,
    ///         RedactPiiAudio = true,
    ///         RedactPiiAudioQuality = RedactPiiAudioQuality.Mp3,
    ///         RedactPiiPolicies = new List&lt;PiiPolicy&gt;()
    ///         {
    ///             PiiPolicy.UsSocialSecurityNumber,
    ///             PiiPolicy.CreditCardNumber,
    ///         },
    ///         RedactPiiSub = SubstitutionPolicy.Hash,
    ///         SpeakerLabels = true,
    ///         SpeakersExpected = 2,
    ///         ContentSafety = true,
    ///         IabCategories = true,
    ///         CustomSpelling = new List&lt;TranscriptCustomSpelling&gt;()
    ///         {
    ///             new TranscriptCustomSpelling
    ///             {
    ///                 From = new List&lt;string&gt;() { "dicarlo" },
    ///                 To = "Decarlo",
    ///             },
    ///         },
    ///         SentimentAnalysis = true,
    ///         AutoChapters = true,
    ///         EntityDetection = true,
    ///         SpeechThreshold = 0.5f,
    ///         Summarization = true,
    ///         SummaryModel = SummaryModel.Informative,
    ///         SummaryType = SummaryType.Bullets,
    ///         CustomTopics = true,
    ///         Topics = new List&lt;string&gt;() { "topics" },
    ///         AudioUrl = "https://assembly.ai/wildfires.mp3",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<Transcript> SubmitAsync(
        TranscriptParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "v2/transcript",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Transcript>(responseBody)!;
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
    /// Get the transcript resource. The transcript is ready when the "status" is "completed".
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.GetAsync("transcript_id");
    /// </code>
    /// </example>
    public async Task<Transcript> GetAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"v2/transcript/{transcriptId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Transcript>(responseBody)!;
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
    /// Remove the data from the transcript and mark it as deleted.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.DeleteAsync("{transcript_id}");
    /// </code>
    /// </example>
    public async Task<Transcript> DeleteAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"v2/transcript/{transcriptId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Transcript>(responseBody)!;
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
    /// Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.GetSubtitlesAsync(
    ///     "string",
    ///     SubtitleFormat.Srt,
    ///     new GetSubtitlesParams { CharsPerCaption = 1 }
    /// );
    /// </code>
    /// </example>
    public async Task<string> GetSubtitlesAsync(
        string transcriptId,
        SubtitleFormat subtitleFormat,
        GetSubtitlesParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.CharsPerCaption != null)
        {
            _query["chars_per_caption"] = request.CharsPerCaption.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,                
                Path = $"v2/transcript/{transcriptId}/{subtitleFormat.Stringify()}",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return responseBody;
        }
        throw new ApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// Get the transcript split by sentences. The API will attempt to semantically segment the transcript into sentences to create more reader-friendly transcripts.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.GetSentencesAsync("transcript_id");
    /// </code>
    /// </example>
    public async Task<SentencesResponse> GetSentencesAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"v2/transcript/{transcriptId}/sentences",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<SentencesResponse>(responseBody)!;
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
    /// Get the transcript split by paragraphs. The API will attempt to semantically segment your transcript into paragraphs to create more reader-friendly transcripts.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.GetParagraphsAsync("transcript_id");
    /// </code>
    /// </example>
    public async Task<ParagraphsResponse> GetParagraphsAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"v2/transcript/{transcriptId}/paragraphs",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ParagraphsResponse>(responseBody)!;
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
    /// Search through the transcript for keywords. You can search for individual words, numbers, or phrases containing up to five words or numbers.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.WordSearchAsync("string", new WordSearchParams { Words = ["string"] });
    /// </code>
    /// </example>
    public async Task<WordSearchResponse> WordSearchAsync(
        string transcriptId,
        WordSearchParams request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["words"] = request.Words;
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"v2/transcript/{transcriptId}/word-search",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<WordSearchResponse>(responseBody)!;
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
    /// Retrieve the redacted audio object containing the status and URL to the redacted audio.
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Transcripts.GetRedactedAudioAsync("transcript_id");
    /// </code>
    /// </example>
    public async Task<RedactedAudioResponse> GetRedactedAudioAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"v2/transcript/{transcriptId}/redacted-audio",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<RedactedAudioResponse>(responseBody)!;
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
