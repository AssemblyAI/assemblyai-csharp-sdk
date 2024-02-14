using AssemblyAI;
using BlazorSample.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSample.Server;

public static class ApiEndpoints
{
    // TODO: Replace when stream is supported by SDK
    public static WebApplication MapApiEndpoints(this WebApplication webApplication)
    {
        var api = webApplication.MapGroup("/api");
        api.MapPost("/transcribe-file", TranscribeFile);
        api.MapPost("/ask-lemur", AskLemur);
        api.MapPost("/realtime/token", GetRealtimeToken);
        return webApplication;
    }

    [RequestSizeLimit(2_306_867_200)]
    private static async Task<Transcript> TranscribeFile([FromForm] TranscribeFileModel model,
        AssemblyAI.AssemblyAI assemblyAIClient)
    {
        await using var fileStream = model.File.OpenReadStream();
        var fileUpload = await assemblyAIClient.Files.Upload(await ReadToEndAsync(fileStream));
        var transcript = await assemblyAIClient.Transcript.Create(new CreateTranscriptParameters
            { AudioUrl = fileUpload.UploadUrl, LanguageCode = new TranscriptLanguageCode(model.LanguageCode) });
        return transcript;
    }

    private static async Task<object> AskLemur(
        [FromForm] string transcriptId,
        [FromForm] string question,
        AssemblyAI.AssemblyAI assemblyAIClient
    )
    {
        var response = await assemblyAIClient.Lemur.Task(new LemurTaskParameters
        {
            TranscriptIds = [transcriptId],
            Prompt = question
        });
        return new { response = response.Response };
    }

    private static async Task<RealtimeTemporaryTokenResponse> GetRealtimeToken(AssemblyAI.AssemblyAI assemblyAIClient)
    {
        var tokenResponse = await assemblyAIClient.Realtime
            .CreateTemporaryToken(new CreateRealtimeTemporaryTokenParameters { ExpiresIn = 360 });
        return tokenResponse;
    }

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

            while ((bytesRead = await stream
                       .ReadAsync(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)
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