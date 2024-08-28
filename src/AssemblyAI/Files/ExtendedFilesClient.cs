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
    public async Task<UploadedFile> UploadAsync(FileInfo audioFile, RequestOptions? options = null)
    {
        using var audioFileStream = audioFile.OpenRead();
        return await UploadAsync(audioFileStream, options).ConfigureAwait(false);
    }

    /// <summary>
    /// Upload a media file to AssemblyAI's servers.
    /// </summary>
    /// <param name="stream">The file stream to upload</param>
    /// <param name="disposeStream">Dispose the stream ASAP</param>
    /// <param name="options">The HTTP request options</param>
    public async Task<UploadedFile> UploadAsync(Stream stream, bool disposeStream, RequestOptions? options = null)
    {
        if (!disposeStream) return await UploadAsync(stream, options).ConfigureAwait(false);
        using (stream)
        {
            return await UploadAsync(stream, options).ConfigureAwait(false);
        }
    }
}
