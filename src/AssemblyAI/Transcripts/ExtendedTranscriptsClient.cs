using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using AssemblyAI.Core;
using AssemblyAI.Files;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace AssemblyAI.Transcripts;

/// <summary>
/// The client to interact with the AssemblyAI Transcripts API.
/// </summary>
public class ExtendedTranscriptsClient : TranscriptsClient
{
    private readonly RawClient _client;
    private readonly AssemblyAIClient _assemblyAIClient;

    internal ExtendedTranscriptsClient(RawClient client, AssemblyAIClient assemblyAIClient) : base(client)
    {
        _client = client;
        _assemblyAIClient = assemblyAIClient;
    }

    /// <summary>
    /// Create a transcript from a local file.
    /// </summary>
    /// <param name="audioFile">The audio file to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        FileInfo audioFile,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFile, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Create a transcript from a local file.
    /// </summary>
    /// <param name="audioFile">The audio file to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
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

    /// <summary>
    /// Create a transcript from a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Create a transcript from a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="disposeStream">Dispose the stream as soon as possible</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        bool disposeStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, disposeStream, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Create a transcript from a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        Stream audioFileStream,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileStream, false, transcriptParams, options, cancellationToken);


    /// <summary>
    /// Create a transcript from a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="disposeStream">Dispose the stream as soon as possible</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
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

    /// <summary>
    /// Create a transcript from an audio file URI.
    /// </summary>
    /// <param name="audioFileUrl">The URI to the audio file to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        Uri audioFileUrl,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(audioFileUrl, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Create a transcript from an audio file URI.
    /// </summary>
    /// <param name="audioFileUrl">The URI to the audio file to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
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

    /// <summary>
    /// Create a transcript from a file uploaded to AssemblyAI.
    /// </summary>
    /// <param name="file">The file uploaded to AssemblyAI</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
    public Task<Transcript> SubmitAsync(
        UploadedFile file,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => SubmitAsync(file, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Create a transcript from a file uploaded to AssemblyAI.
    /// </summary>
    /// <param name="file">The file uploaded to AssemblyAI</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a task that resolves to a queued transcript</returns>
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

    /// <summary>
    /// Transcribe a local file
    /// </summary>
    /// <param name="audioFile">The local file to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        FileInfo audioFile,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFile, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Transcribe a local file
    /// </summary>
    /// <param name="audioFile">The local file to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
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

    /// <summary>
    /// Transcribe a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileStream, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Transcribe a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="disposeStream">Dispose the stream as soon as possible</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        Stream audioFileStream,
        bool disposeStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileStream, disposeStream, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Transcribe a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
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

    /// <summary>
    /// Transcribe a file stream.
    /// </summary>
    /// <param name="audioFileStream">The audio file stream to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="disposeStream">Dispose the stream as soon as possible</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
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

    /// <summary>
    /// Transcribe an audio file via its public URI.
    /// </summary>
    /// <param name="audioFileUrl">The URI to the audio file to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        Uri audioFileUrl,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(audioFileUrl, new TranscriptOptionalParams(), options, cancellationToken);

    /// <summary>
    /// Transcribe an audio file via its public URI.
    /// </summary>
    /// <param name="audioFileUrl">The URI to the audio file to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        Uri audioFileUrl,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(CreateParams(audioFileUrl, transcriptParams), options, cancellationToken);

    /// <summary>
    /// Transcribe a file uploaded to AssemblyAI.
    /// </summary>
    /// <param name="file">The file uploaded to AssemblyAI to transcribe</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        UploadedFile file,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(file, new TranscriptOptionalParams(), options, cancellationToken);


    /// <summary>
    /// Transcribe a file uploaded to AssemblyAI.
    /// </summary>
    /// <param name="file">The file uploaded to AssemblyAI to transcribe</param>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public Task<Transcript> TranscribeAsync(
        UploadedFile file,
        TranscriptOptionalParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => TranscribeAsync(CreateParams(file.UploadUrl, transcriptParams), options, cancellationToken);

    /// <summary>
    /// Transcribe an audio file via its public URI.
    /// </summary>
    /// <param name="transcriptParams">The transcript parameters</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that resolves to a transcript with status "completed" or "error".</returns>
    public async Task<Transcript> TranscribeAsync(
        TranscriptParams transcriptParams,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var transcript = await SubmitAsync(transcriptParams, options, cancellationToken).ConfigureAwait(false);
        transcript = await WaitUntilReadyAsync(transcript.Id, null, null, options, cancellationToken)
            .ConfigureAwait(false);
        return transcript;
    }

    [Obsolete("Use `WaitUntilReadyAsync` instead.")]
    public Task<Transcript> WaitUntilReady(
        string id,
        TimeSpan? pollingInterval = null,
        TimeSpan? pollingTimeout = null,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => WaitUntilReadyAsync(id, pollingInterval, pollingTimeout, options, cancellationToken);

    /// <summary>
    /// Wait until the transcript status is either "completed" or "error".
    /// </summary>
    /// <param name="id">The transcript ID</param>
    /// <param name="pollingInterval">How frequently the transcript is polled. Defaults to 3s.</param>
    /// <param name="pollingTimeout">How long to wait until the timeout exception thrown. Defaults to infinite.</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The transcript with status "completed" or "error"</returns>
    public async Task<Transcript> WaitUntilReadyAsync(
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

    /// <summary>
    /// Create transcript parameters from an audio file URL and optional parameters.
    /// </summary>
    /// <param name="audioFileUrl">The audio file URL to transcribe</param>
    /// <param name="optionalTranscriptParams">The optional transcript parameters</param>
    /// <returns>The transcript parameters</returns>
    private static TranscriptParams CreateParams(Uri audioFileUrl, TranscriptOptionalParams optionalTranscriptParams)
        => optionalTranscriptParams.ToTranscriptParams(audioFileUrl);

    /// <inheritdoc cref="CreateParams(Uri,TranscriptOptionalParams)"/>
    private static TranscriptParams CreateParams(string audioFileUrl, TranscriptOptionalParams optionalTranscriptParams)
        => optionalTranscriptParams.ToTranscriptParams(audioFileUrl);

    /// <summary>
    /// Retrieve a list of transcripts you created.
    /// Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
    /// </summary>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of transcripts you created</returns>
    public Task<TranscriptList> ListAsync(RequestOptions? options = null, CancellationToken cancellationToken = default)
        => ListAsync(new ListTranscriptParams(), options, cancellationToken);

    /// <summary>
    /// Retrieve a list of transcripts you created.
    /// Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
    /// </summary>
    /// <param name="listUrl">The next or previous page URL to query the transcript list.</param>
    /// <param name="options">HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of transcripts you created</returns>
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
    /// <returns>A task that resolves to a string of the subtitles</returns>
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
    /// <returns>A task that resolves to a string of the subtitles</returns>
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
    /// <returns>A task that resolves to a stream of the redacted audio file</returns>
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