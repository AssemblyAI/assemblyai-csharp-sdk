using System.Net.Http.Json;
using AssemblyAI;
using AssemblyAI.Files;
using AssemblyAI.Transcripts;
using BlazorSample.Shared;
using BlazorSample.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorSample.Wasm;

public class FileTranscriber(
    HttpClient httpClient,
    AntiforgeryStateProvider antiforgeryStateProvider)
    : IFileTranscriber
{
    public async Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model)
    {
        var csrfToken = antiforgeryStateProvider.GetAntiforgeryToken();
        await using var fileStream = model.File.OpenReadStream(maxAllowedSize: 2_306_867_200);
        var content = new MultipartFormDataContent
        {
            { new StreamContent(fileStream), nameof(model.File), model.File.Name }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/file");
        request.Headers.Add("RequestVerificationToken", csrfToken.Value);
        request.Content = content;
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var fileUpload = await response.Content.ReadFromJsonAsync<UploadedFile>()!;
        
        TranscriptLanguageCode? langCode = null;
        bool? useAld = null;
        if (model.LanguageCode == "ALD")
        {
            useAld = true;
        }
        else
        {
            langCode = EnumConverter.ToEnum<TranscriptLanguageCode>(model.LanguageCode);
        }
        request = new HttpRequestMessage(HttpMethod.Post, "/api/transcript");
        request.Headers.Add("RequestVerificationToken", csrfToken.Value);
        request.Content = JsonContent.Create(new TranscriptParams
        {
            AudioUrl = fileUpload.UploadUrl,
            LanguageCode = langCode,
            LanguageDetection = useAld
        });
        response = await httpClient.SendAsync(request);
        
        var transcript = await response.Content.ReadFromJsonAsync<Transcript>()!;
        while (transcript.Status != TranscriptStatus.Completed && transcript.Status != TranscriptStatus.Error)
        {
            await Task.Delay(1000);
            request = new HttpRequestMessage(HttpMethod.Get, $"/api/transcript/{transcript.Id}");
            request.Headers.Add("RequestVerificationToken", csrfToken.Value);
            response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            transcript = await response.Content.ReadFromJsonAsync<Transcript>()!;
        }

        return transcript;
    }
}