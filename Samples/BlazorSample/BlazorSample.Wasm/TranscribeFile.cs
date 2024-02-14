using System.Net.Http.Json;
using AssemblyAI;
using BlazorSample.Shared;
using BlazorSample.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorSample.Wasm;

public class TranscribeFile(
    HttpClient httpClient,
    AntiforgeryStateProvider antiforgeryStateProvider)
    : ITranscribeFile
{
    public async Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model)
    {
        var csrfToken = antiforgeryStateProvider.GetAntiforgeryToken();
        await using var fileStream = model.File.OpenReadStream(maxAllowedSize: 2_306_867_200);
        var content = new MultipartFormDataContent
        {
            { new StringContent(model.LanguageCode), nameof(model.LanguageCode) },
            { new StreamContent(fileStream), nameof(model.File), model.File.Name }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/transcribe-file");
        request.Headers.Add("RequestVerificationToken", csrfToken.Value);
        request.Content = content;
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Transcript>() ?? throw new InvalidOperationException();
    }
}