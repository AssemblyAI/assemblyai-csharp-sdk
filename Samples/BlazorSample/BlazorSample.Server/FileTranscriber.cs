using AssemblyAI;
using AssemblyAI.Transcripts;
using BlazorSample.Shared;
using BlazorSample.Shared.Models;

namespace BlazorSample.Server;

public class FileTranscriber : IFileTranscriber
{
    private readonly AssemblyAIClient _assemblyAIClient;

    public FileTranscriber(AssemblyAIClient assemblyAIClient)
    {
        _assemblyAIClient = assemblyAIClient;
    }

    public async Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model)
    {
        await using var fileStream = model.File.OpenReadStream(maxAllowedSize: 2_306_867_200);
        var transcript = await _assemblyAIClient.Transcripts.SubmitAsync(fileStream, new TranscriptOptionalParams
        {
            LanguageCode = EnumConverter.ToEnum<TranscriptLanguageCode>(model.LanguageCode)
        });
        return transcript;
    }
}