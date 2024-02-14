using System.Text.Json;
using AssemblyAI;
using BlazorSample.Shared;

namespace BlazorSample.Server;

public class AskLemur(AssemblyAI.AssemblyAI assemblyAIClient)
    : IAskLemur
{
    public async Task<string> AskQuestionAsync(string transcriptId, string question)
    {
        var response = await assemblyAIClient.Lemur.Task(new LemurTaskParameters
        {
            TranscriptIds = [transcriptId],
            Prompt = question
        });
        return response.Response;
    }
}