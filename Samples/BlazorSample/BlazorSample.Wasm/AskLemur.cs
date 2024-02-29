using System.Net.Http.Json;
using System.Text.Json;
using BlazorSample.Shared;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorSample.Wasm;

public class AskLemur(HttpClient httpClient, AntiforgeryStateProvider antiforgeryStateProvider)
    : IAskLemur
{
    public async Task<string> AskQuestionAsync(string transcriptId, string question)
    {
        var csrfToken = antiforgeryStateProvider.GetAntiforgeryToken()!;
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/ask-lemur");
        request.Headers.Add("RequestVerificationToken", csrfToken.Value);
        request.Content = JsonContent.Create(new
        {
            transcriptId,
            question
        });
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = (await response.Content.ReadFromJsonAsync<JsonDocument>())!;
        return json.RootElement.GetProperty("response").GetString()!;
    }
}