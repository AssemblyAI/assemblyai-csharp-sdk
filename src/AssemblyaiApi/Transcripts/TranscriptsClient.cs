using System.Text.Json;
using AssemblyaiApi;

namespace AssemblyaiApi;

public class TranscriptsClient
{
    private RawClient _client;

    public TranscriptsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Retrieve a list of transcripts you created.
    /// Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
    /// </summary>
    public async Task<TranscriptList> ListAsync(ListTranscriptParams request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Status != null)
        {
            _query["status"] = request.Status;
        }
        if (request.CreatedOn != null)
        {
            _query["created_on"] = request.CreatedOn;
        }
        if (request.BeforeID != null)
        {
            _query["before_id"] = request.BeforeID;
        }
        if (request.AfterID != null)
        {
            _query["after_id"] = request.AfterID;
        }
        if (request.ThrottledOnly != null)
        {
            _query["throttled_only"] = request.ThrottledOnly;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/v2/transcript",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<TranscriptList>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Create a transcript from a media file that is accessible via a URL.
    /// </summary>
    public async Task<Transcript> SubmitAsync(TranscriptParams request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/v2/transcript",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<Transcript>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Get the transcript resource. The transcript is ready when the "status" is "completed".
    /// </summary>
    public async Task<Transcript> GetAsync(string transcriptID)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<Transcript>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Delete the transcript.
    /// Deleting does not delete the resource itself, but removes the data from the resource and marks it as deleted.
    /// </summary>
    public async Task<Transcript> DeleteAsync(string transcriptID)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/v2/transcript/{transcriptID}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<Transcript>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
    /// </summary>
    public async void GetSubtitlesAsync(
        string transcriptID,
        SubtitleFormat subtitleFormat,
        GetSubtitlesParams request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CharsPerCaption != null)
        {
            _query["chars_per_caption"] = request.CharsPerCaption;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}/{subtitleFormat}",
                Query = _query
            }
        );
    }

    /// <summary>
    /// Get the transcript split by sentences. The API will attempt to semantically segment the transcript into sentences to create more reader-friendly transcripts.
    /// </summary>
    public async Task<SentencesResponse> GetSentencesAsync(string transcriptID)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}/sentences"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<SentencesResponse>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Get the transcript split by paragraphs. The API will attempt to semantically segment your transcript into paragraphs to create more reader-friendly transcripts.
    /// </summary>
    public async Task<ParagraphsResponse> GetParagraphsAsync(string transcriptID)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}/paragraphs"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ParagraphsResponse>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Search through the transcript for keywords. You can search for individual words, numbers, or phrases containing up to five words or numbers.
    /// </summary>
    public async Task<WordSearchResponse> WordSearchAsync(
        string transcriptID,
        WordSearchParams request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Words != null)
        {
            _query["words"] = request.Words;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}/word-search",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<WordSearchResponse>(responseBody);
        }
        throw new Exception();
    }

    /// <summary>
    /// Retrieve the redacted audio object containing the status and URL to the redacted audio.
    /// </summary>
    public async Task<RedactedAudioResponse> GetRedactedAudioAsync(string transcriptID)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptID}/redacted-audio"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<RedactedAudioResponse>(responseBody);
        }
        throw new Exception();
    }
}
