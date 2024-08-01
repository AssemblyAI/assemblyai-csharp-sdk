using AssemblyAI;
using AssemblyAI.Transcripts;
using BlazorSample.Shared;
using BlazorSample.Shared.Models;

namespace BlazorSample.Server;

public class FileTranscriber(AssemblyAIClient assemblyAIClient) : IFileTranscriber
{
    public async Task<Transcript> TranscribeFileAsync(TranscribeFileFormModel model)
    {
        await using var fileStream = model.File.OpenReadStream(maxAllowedSize: 2_306_867_200);
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
        var transcript = await assemblyAIClient.Transcripts.SubmitAsync(fileStream, new TranscriptOptionalParams
        {
            LanguageCode = langCode,
            LanguageDetection = useAld
        });
        return transcript;
    }
}