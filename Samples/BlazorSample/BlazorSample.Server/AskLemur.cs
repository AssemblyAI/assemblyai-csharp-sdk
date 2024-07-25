using System.Text.Json;
using AssemblyAI;
using AssemblyAI.Lemur;
using BlazorSample.Shared;

namespace BlazorSample.Server;

public class AskLemur(AssemblyAIClient assemblyAIClient)
    : IAskLemur
{
    public async Task<string> AskQuestionAsync(string transcriptId, string question)
    {
        var response = await assemblyAIClient.Lemur.TaskAsync(new LemurTaskParams
        {
            TranscriptIds = [transcriptId],
            Prompt = question
        });
        return response.Response;
    }
}