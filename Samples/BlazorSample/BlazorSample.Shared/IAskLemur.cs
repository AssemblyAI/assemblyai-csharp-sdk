namespace BlazorSample.Shared;

public interface IAskLemur
{
    public Task<string> AskQuestionAsync(string transcriptId, string question);
}