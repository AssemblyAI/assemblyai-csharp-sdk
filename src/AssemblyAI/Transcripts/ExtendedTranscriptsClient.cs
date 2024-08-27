using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using AssemblyAI.Core;
using AssemblyAI.Files;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace AssemblyAI.Transcripts;

public class ExtendedTranscriptsClient : TranscriptsClient
{
    private readonly RawClient _client;
    private readonly AssemblyAIClient _assemblyAIClient;

    internal ExtendedTranscriptsClient(RawClient client, AssemblyAIClient assemblyAIClient) : base(client)
    {
        _client = client;
        _assemblyAIClient = assemblyAIClient;
    }

    public Task<Transcript> SubmitAsync(
        FileInfo audioFile,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFile, new TranscriptOptionalParams(), options, cancellationToken);

    public async Task<Transcript> SubmitAsync(
        FileInfo audioFile,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var uploadedFile = await _assemblyAIClient.Files.UploadAsync(audioFile, options, cancellationToken)
            .ConfigureAwait(false);
        return await SubmitAsync(uploadedFile, transcriptParams, options, cancellationToken).ConfigureAwait(false);
    }

    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, new TranscriptOptionalParams(), options, cancellationToken);

    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        bool disposeStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, disposeStream, new TranscriptOptionalParams(), options, cancellationToken);

    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, false, transcriptParams, options, cancellationToken);

    public async Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        bool disposeStream,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var fileUpload = await _assemblyAIClient.Files
            .UploadAsync(audioFileStream, disposeStream, options, cancellationToken)
            .ConfigureAwait(false);
        return await SubmitAsync(fileUpload, transcriptParams, options, cancellationToken).ConfigureAwait(false);
    }

    public Task<Transcript> SubmitAsync(
        Uri audioFileUrl,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileUrl, new TranscriptOptionalParams(), options, cancellationToken);

    public async Task<Transcript> SubmitAsync(
        Uri audioFileUrl,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await SubmitAsync(CreateParams(audioFileUrl, transcriptParams), options, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task<Transcript> SubmitAsync(
        UploadedFile file,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(file, new TranscriptOptionalParams(), options, cancellationToken);

    public async Task<Transcript> SubmitAsync(
        UploadedFile file,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await SubmitAsync(CreateParams(file.UploadUrl, transcriptParams), options, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(
        FileInfo audioFile,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFile, new TranscriptOptionalParams(), options, cancellationToken);

    public async Task<Transcript> TranscribeAsync(
        FileInfo audioFile,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
#if NET6_0_OR_GREATER
        await using var audioFileStream = audioFile.OpenRead();
#else
        using var audioFileStream = audioFile.OpenRead();
#endif
        return await TranscribeAsync(audioFileStream, transcriptParams, options, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileStream, new TranscriptOptionalParams(), options, cancellationToken);

    public Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        bool disposeStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileStream, disposeStream, new TranscriptOptionalParams(), options, cancellationToken);

    public async Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var fileUpload = await _assemblyAIClient.Files
            .UploadAsync(audioFileStream, options, cancellationToken).ConfigureAwait(false);
        return await TranscribeAsync(new Uri(fileUpload.UploadUrl), transcriptParams, options, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        bool disposeStream,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var uploadedFile = await _assemblyAIClient.Files.UploadAsync(
                audioFileStream,
                disposeStream,
                options,
                cancellationToken
            )
            .ConfigureAwait(false);
        return await TranscribeAsync(uploadedFile, transcriptParams, options, cancellationToken).ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(
        Uri audioFileUrl,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileUrl, new TranscriptOptionalParams(), options, cancellationToken);

    public Task<Transcript> TranscribeAsync(
        Uri audioFileUrl,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(CreateParams(audioFileUrl, transcriptParams), options, cancellationToken);

    public Task<Transcript> TranscribeAsync(
        UploadedFile file,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(file, new TranscriptOptionalParams(), options, cancellationToken);

    public Task<Transcript> TranscribeAsync(
        UploadedFile file,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(CreateParams(file.UploadUrl, transcriptParams), options, cancellationToken);

    public async Task<Transcript> TranscribeAsync(
        TranscriptParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var transcript = await SubmitAsync(transcriptParams, options, cancellationToken).ConfigureAwait(false);
        transcript = await WaitUntilReady(transcript.Id, null, null, options, cancellationToken)
            .ConfigureAwait(false);
        return transcript;
    }

    /// <summary>
    /// Wait until the transcript status is either "completed" or "error".
    /// </summary>
    /// <param name="id">The transcript ID</param>
    /// <param name="pollingInterval">How frequently the transcript is polled. Defaults to 3s.</param>
    /// <param name="pollingTimeout">How long to wait until the timeout exception thrown. Defaults to infinite.</param>
    /// <param name="options"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The transcript with status "completed" or "error"</returns>
    public async Task<Transcript> WaitUntilReady(
        string id,
        TimeSpan? pollingInterval = null,
        TimeSpan? pollingTimeout = null,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        using var combinedCts = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken, pollingTimeout == null
                ? CancellationToken.None
                : new CancellationTokenSource(pollingTimeout.Value).Token
        );
        var ct = combinedCts.Token;

        var transcript = await GetAsync(id, options, ct).ConfigureAwait(false);
        while (transcript.Status != TranscriptStatus.Completed && transcript.Status != TranscriptStatus.Error)
        {
            if (ct.IsCancellationRequested)
            {
                throw new TimeoutException("The transcript did not complete within the given timeout.");
            }

            try
            {
                await Task.Delay(pollingInterval ?? TimeSpan.FromSeconds(3), ct).ConfigureAwait(false);
            }
            catch (TaskCanceledException e)
            {
                throw new TimeoutException("The transcript did not complete within the given timeout.", e);
            }

            transcript = await GetAsync(transcript.Id, options, ct).ConfigureAwait(false);
        }

        return transcript;
    }

    private static TranscriptParams CreateParams(Uri audioFileUrl, TranscriptOptionalParams optionalTranscriptParams)
        => CreateParams(audioFileUrl.ToString(), optionalTranscriptParams);

    private static TranscriptParams CreateParams(string audioFileUrl, TranscriptOptionalParams optionalTranscriptParams)
    {
        var json = JsonUtils.Serialize(optionalTranscriptParams);
        var jsonObject = JsonUtils.Deserialize<JsonObject>(json);
        jsonObject["audio_url"] = audioFileUrl;
        var transcriptParams = jsonObject.Deserialize<TranscriptParams>()!;
        return transcriptParams;
    }

    /// <summary>
    /// Get the transcript resource. The transcript is ready when the "status" is "completed".
    /// </summary>
    public Task<TranscriptList> ListAsync(
        RequestOptions? options = null, CancellationToken cancellationToken = default)
        => ListAsync(new ListTranscriptParams(), options, cancellationToken);

    /// <summary>
    /// Retrieve a list of transcripts you created.
    /// Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
    /// </summary>
    /// <param name="listUrl">The next or previous page URL to query the transcript list.</param>
    /// <param name="options"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<TranscriptList> ListAsync(string listUrl,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(listUrl))
            throw new ArgumentNullException(nameof(listUrl), "listUrl parameter is null or empty.");

        // this would be easier to just call the given URL,
        // but the raw client doesn't let us make requests to full URL
        // so, we'll parse the querystring and pass it to `ListAsync`.

        var queryString = listUrl.Substring(listUrl.IndexOf('?') + 1)
            .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(k => k.Split('='))
            .Where(k => k.Length == 2)
            .ToLookup(a => a[0], a => Uri.UnescapeDataString(a[1])
                , StringComparer.OrdinalIgnoreCase);
        var listTranscriptParams = new ListTranscriptParams();
        if (queryString.Contains("limit"))
        {
            listTranscriptParams.Limit = int.Parse(queryString["limit"].First());
        }

        if (queryString.Contains("status"))
        {
            listTranscriptParams.Status =
                (TranscriptStatus)Enum.Parse(typeof(TranscriptStatus), queryString["limit"].First());
        }

        if (queryString.Contains("created_on"))
        {
            listTranscriptParams.CreatedOn = queryString["created_on"].First();
        }

        if (queryString.Contains("before_id"))
        {
            listTranscriptParams.BeforeId = queryString["before_id"].First();
        }

        if (queryString.Contains("after_id"))
        {
            listTranscriptParams.AfterId = queryString["after_id"].First();
        }

        if (queryString.Contains("throttled_only"))
        {
            listTranscriptParams.ThrottledOnly = bool.Parse(queryString["throttled_only"].First());
        }

        return await ListAsync(listTranscriptParams, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
    /// </summary>
    public Task<string> GetSubtitlesAsync(
        string transcriptId,
        SubtitleFormat subtitleFormat,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
        => GetSubtitlesAsync(transcriptId, subtitleFormat, new GetSubtitlesParams(), options, cancellationToken);

    /// <summary>
    /// Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
    /// </summary>
    public Task<string> GetSubtitlesAsync(
        string transcriptId,
        SubtitleFormat subtitleFormat,
        int charsPerCaption,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
        => GetSubtitlesAsync(transcriptId, subtitleFormat, new GetSubtitlesParams
        {
            CharsPerCaption = charsPerCaption
        }, options, cancellationToken);

    /// <summary>
    /// Retrieve the redacted audio file.
    /// </summary>
    public async Task<Stream> GetRedactedAudioFileAsync(
        string transcriptId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var redactedAudioFileInfo =
            await GetRedactedAudioAsync(transcriptId, options, cancellationToken).ConfigureAwait(false);
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        var httpClient = options?.HttpClient ?? _client.Options.HttpClient ?? new HttpClient();
#if NET6_0_OR_GREATER
        return await httpClient.GetStreamAsync(redactedAudioFileInfo.RedactedAudioUrl, cancellationToken)
            .ConfigureAwait(false);
#else
        return await httpClient.GetStreamAsync(redactedAudioFileInfo.RedactedAudioUrl)
            .ConfigureAwait(false);
#endif
    }

    /// <summary>
    /// Search through the transcript for keywords. You can search for individual words, numbers, or phrases containing up to five words or numbers.
    /// </summary>
    public Task<WordSearchResponse> WordSearchAsync(
        string transcriptId,
        string[] words,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => WordSearchAsync(transcriptId, new WordSearchParams
    {
        Words = words
    }, options, cancellationToken);
}