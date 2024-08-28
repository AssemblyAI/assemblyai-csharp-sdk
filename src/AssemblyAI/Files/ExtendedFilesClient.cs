namespace AssemblyAI.Files;

/// <summary>
/// Client to upload files to the AssemblyAI API.
/// </summary>
public partial class FilesClient
{
    /// <summary>
    /// Upload a media file to AssemblyAI's servers.
    /// </summary>
    /// <param name="audioFile">The local file to upload</param>
    /// <param name="options">The HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>File uploaded to AssemblyAI</returns>
    public async Task<UploadedFile> UploadAsync(
        FileInfo audioFile,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
#if NET6_0_OR_GREATER
        await using var audioFileStream = audioFile.OpenRead();
#else
        using var audioFileStream = audioFile.OpenRead();
#endif

        return await UploadAsync(audioFileStream, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Upload a media file to AssemblyAI's servers.
    /// </summary>
    /// <param name="stream">The file stream to upload</param>
    /// <param name="disposeStream">Dispose the stream ASAP</param>
    /// <param name="options">The HTTP request options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>File uploaded to AssemblyAI</returns>
    public async Task<UploadedFile> UploadAsync(
        Stream stream,
        bool disposeStream,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        if (!disposeStream) return await UploadAsync(stream, options, cancellationToken).ConfigureAwait(false);
#if NET6_0_OR_GREATER
        await using (stream)
#else
        using (stream)
#endif
        {
            return await UploadAsync(stream, options, cancellationToken).ConfigureAwait(false);
        }
    }
}