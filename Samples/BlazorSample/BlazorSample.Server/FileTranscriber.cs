using AssemblyAI;
using BlazorSample.Shared;
using BlazorSample.Shared.Models;

namespace BlazorSample.Server;

public class FileTranscriber : IFileTranscriber
{
    private readonly AssemblyAI.AssemblyAI _assemblyAIClient;

    public FileTranscriber(AssemblyAI.AssemblyAI assemblyAIClient)
    {
        _assemblyAIClient = assemblyAIClient;
    }

    public async Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model)
    {
        await using var fileStream = model.File.OpenReadStream(maxAllowedSize: 2_306_867_200);
        var fileUpload = await _assemblyAIClient.Files.Upload(await ReadToEndAsync(fileStream));
        var transcript = await _assemblyAIClient.Transcript.Create(new CreateTranscriptParameters
        {
            AudioUrl = fileUpload.UploadUrl,
            LanguageCode = new TranscriptLanguageCode(model.LanguageCode)
        });
        return transcript;
    }
    
    // TODO: Replace when stream is supported by SDK
    private static async Task<byte[]> ReadToEndAsync(Stream stream)
    {
        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        var totalBytesRead = 0;
        try
        {
            var readBuffer = new byte[4096];

            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)
                       .ConfigureAwait(false)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead != readBuffer.Length) continue;
                var nextByte = stream.ReadByte();
                if (nextByte == -1) continue;
                var temp = new byte[readBuffer.Length * 2];
                Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                readBuffer = temp;
                totalBytesRead++;
            }

            var buffer = readBuffer;
            if (readBuffer.Length == totalBytesRead) return buffer;
            buffer = new byte[totalBytesRead];
            Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);

            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }
}