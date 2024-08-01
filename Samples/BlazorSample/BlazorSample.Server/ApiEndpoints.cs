using AssemblyAI;
using AssemblyAI.Files;
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
        api.MapPost("/file", UploadFile);
        api.MapPost("/transcript", CreateTranscript);
        api.MapGet("/transcript/{transcriptId}", GetTranscript);
        api.MapPost("/ask-lemur", AskLemur);
        api.MapPost("/realtime/token", GetRealtimeToken);
        return webApplication;
    }

    [RequestSizeLimit(2_306_867_200)]
    private static async Task<UploadedFile> UploadFile(
        [FromForm] UploadFileForm form,
        AssemblyAIClient assemblyAIClient
    )
    {
        await using var fileStream = form.File.OpenReadStream();
        var uploadedFile = await assemblyAIClient.Files.UploadAsync(fileStream);
        return uploadedFile;
    }
    
    private static async Task<Transcript> CreateTranscript(
        [FromBody] TranscriptParams transcriptParams,
        AssemblyAIClient assemblyAIClient
    )
    {
        var transcript = await assemblyAIClient.Transcripts.SubmitAsync(transcriptParams);
        return transcript;
    }
    
    private static async Task<Transcript> GetTranscript(
        [FromRoute] string transcriptId,
        AssemblyAIClient assemblyAIClient)
    {
        return await assemblyAIClient.Transcripts.GetAsync(transcriptId);
    }

    private static async Task<LemurTaskResponse> AskLemur(
        [FromBody] LemurTaskParams lemurTaskParams,
        AssemblyAIClient assemblyAIClient
    )
    {
        var response = await assemblyAIClient.Lemur.TaskAsync(lemurTaskParams);
        return response;
    }

    private static async Task<RealtimeTemporaryTokenResponse> GetRealtimeToken(AssemblyAIClient assemblyAIClient)
    {
        var tokenResponse = await assemblyAIClient.Realtime
            .CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams { ExpiresIn = 360 });
        return tokenResponse;
    }
}