#nullable enable

namespace AssemblyAI;

public partial class TranscriptsClient
{
    public Task<Transcript> SubmitAsync(FileInfo audioFile) => SubmitAsync(audioFile, new TranscriptOptionalParams());

    public async Task<Transcript> SubmitAsync(FileInfo audioFile, TranscriptOptionalParams transcriptParams)
    {
        using var audioFileStream = audioFile.OpenRead();
        return await SubmitAsync(audioFileStream, transcriptParams).ConfigureAwait(false);
    }

    public Task<Transcript> SubmitAsync(Stream audioFileStream) => SubmitAsync(audioFileStream, new TranscriptOptionalParams());

    public async Task<Transcript> SubmitAsync(Stream audioFileStream, TranscriptOptionalParams transcriptParams)
    {
        var fileUpload = await _assemblyAIClient.Files.UploadAsync(audioFileStream).ConfigureAwait(false);
        return await SubmitAsync(new Uri(fileUpload.UploadUrl), transcriptParams).ConfigureAwait(false);
    }

    public Task<Transcript> SubmitAsync(Uri audioFileUrl) => SubmitAsync(audioFileUrl, new TranscriptOptionalParams());

    public async Task<Transcript> SubmitAsync(Uri audioFileUrl, TranscriptOptionalParams transcriptParams)
    {
        return await SubmitAsync(CreateParams(audioFileUrl, transcriptParams)).ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(FileInfo audioFile) => TranscribeAsync(audioFile, new TranscriptOptionalParams());

    public async Task<Transcript> TranscribeAsync(FileInfo audioFile, TranscriptOptionalParams transcriptParams)
    {
        using var audioFileStream = audioFile.OpenRead();
        return await TranscribeAsync(audioFileStream, transcriptParams).ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(Stream audioFileStream) =>
        TranscribeAsync(audioFileStream, new TranscriptOptionalParams());

    public async Task<Transcript> TranscribeAsync(Stream audioFileStream, TranscriptOptionalParams transcriptParams)
    {
        var fileUpload = await _assemblyAIClient.Files.UploadAsync(audioFileStream).ConfigureAwait(false);
        return await TranscribeAsync(new Uri(fileUpload.UploadUrl), transcriptParams).ConfigureAwait(false);
    }

    public Task<Transcript> TranscribeAsync(Uri audioFileUrl) => TranscribeAsync(audioFileUrl, new TranscriptOptionalParams());

    public async Task<Transcript> TranscribeAsync(Uri audioFileUrl, TranscriptOptionalParams transcriptParams)
    {
        var transcript = await SubmitAsync(CreateParams(audioFileUrl, transcriptParams)).ConfigureAwait(false);
        transcript = await WaitUntilReady(transcript.Id).ConfigureAwait(false);
        return transcript;
    }
    
    public async Task<Transcript> WaitUntilReady(string id)
    {
        var transcript = await GetAsync(id).ConfigureAwait(false);
        while (transcript.Status != TranscriptStatus.Completed && transcript.Status != TranscriptStatus.Error)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            transcript = await GetAsync(transcript.Id).ConfigureAwait(false);
        }

        return transcript;
    }

    private TranscriptParams CreateParams(Uri audioFileUrl, TranscriptOptionalParams transcriptParams)
    {
        return new TranscriptParams
        {
            AudioUrl = audioFileUrl.ToString()
            // TODO: map other parameters
        };
    }
}