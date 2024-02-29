using AssemblyAI;
using BlazorSample.Shared.Models;

namespace BlazorSample.Shared;

public interface IFileTranscriber
{
    public Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model);
}