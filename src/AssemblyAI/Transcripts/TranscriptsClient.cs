using System.Text.Json;
using AssemblyAI;

#nullable enable

namespace AssemblyAI;

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
        throw new Exception(responseBody);
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
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Get the transcript resource. The transcript is ready when the "status" is "completed".
    /// </summary>
    public async Task<Transcript> GetAsync(string transcriptId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<Transcript>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Delete the transcript.
    /// Deleting does not delete the resource itself, but removes the data from the resource and marks it as deleted.
    /// </summary>
    public async Task<Transcript> DeleteAsync(string transcriptId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/v2/transcript/{transcriptId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<Transcript>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
    /// </summary>
    public async void GetSubtitlesAsync(
        string transcriptId,
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
                Path = $"/v2/transcript/{transcriptId}/{subtitleFormat}",
                Query = _query
            }
        );
    }

    /// <summary>
    /// Get the transcript split by sentences. The API will attempt to semantically segment the transcript into sentences to create more reader-friendly transcripts.
    /// </summary>
    public async Task<SentencesResponse> GetSentencesAsync(string transcriptId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptId}/sentences"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<SentencesResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Get the transcript split by paragraphs. The API will attempt to semantically segment your transcript into paragraphs to create more reader-friendly transcripts.
    /// </summary>
    public async Task<ParagraphsResponse> GetParagraphsAsync(string transcriptId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptId}/paragraphs"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ParagraphsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Search through the transcript for keywords. You can search for individual words, numbers, or phrases containing up to five words or numbers.
    /// </summary>
    public async Task<WordSearchResponse> WordSearchAsync(
        string transcriptId,
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
                Path = $"/v2/transcript/{transcriptId}/word-search",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<WordSearchResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Retrieve the redacted audio object containing the status and URL to the redacted audio.
    /// </summary>
    public async Task<RedactedAudioResponse> GetRedactedAudioAsync(string transcriptId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/v2/transcript/{transcriptId}/redacted-audio"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<RedactedAudioResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
