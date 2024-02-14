using AssemblyAI;
using BlazorSample.Shared.Models;

namespace BlazorSample.Shared;

public interface ITranscribeFile
{
    public Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model);
}