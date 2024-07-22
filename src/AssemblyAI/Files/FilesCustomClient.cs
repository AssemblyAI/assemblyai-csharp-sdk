using System.Net.Http;
using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

public partial class FilesClient
{
    /// <summary>
    /// Upload a media file to AssemblyAI's servers.
    /// </summary>
    public async Task<UploadedFile> UploadAsync(FileInfo audioFile)
    {
        using var audioFileStream = audioFile.OpenRead();
        return await UploadAsync(audioFileStream).ConfigureAwait(false);
    }
}
