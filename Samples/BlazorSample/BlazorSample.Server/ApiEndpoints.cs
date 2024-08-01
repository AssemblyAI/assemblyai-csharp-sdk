using AssemblyAI;
using AssemblyAI.Lemur;
using AssemblyAI.Realtime;
using AssemblyAI.Transcripts;
using BlazorSample.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSample.Server;

public static class ApiEndpoints
{
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
        AssemblyAIClient assemblyAIClient)
    {
        await using var fileStream = model.File.OpenReadStream();
        var transcript = await assemblyAIClient.Transcripts.SubmitAsync(fileStream, new TranscriptOptionalParams
        {
            LanguageCode = EnumConverter.ToEnum<TranscriptLanguageCode>(model.LanguageCode)
        });
        return transcript;
    }

    private static async Task<object> AskLemur(
        [FromForm] string transcriptId,
        [FromForm] string question,
        AssemblyAIClient assemblyAIClient
    )
    {
        var response = await assemblyAIClient.Lemur.TaskAsync(new LemurTaskParams
        {
            TranscriptIds = [transcriptId],
            Prompt = question
        });
        return new { response = response.Response };
    }

    private static async Task<RealtimeTemporaryTokenResponse> GetRealtimeToken(AssemblyAIClient assemblyAIClient)
    {
        var tokenResponse = await assemblyAIClient.Realtime
            .CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams { ExpiresIn = 360 });
        return tokenResponse;
    }
}