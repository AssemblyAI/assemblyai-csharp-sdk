using AssemblyAI;
using AssemblyAI.Transcripts;
using BlazorSample.Shared.Models;

namespace BlazorSample.Shared;

public interface IFileTranscriber
{
    public Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model);
}