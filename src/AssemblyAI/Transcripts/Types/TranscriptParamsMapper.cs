using Riok.Mapperly.Abstractions;

// ReSharper disable CheckNamespace

namespace AssemblyAI.Transcripts;

[Mapper]
internal static partial class TranscriptParamsMapper
{
    static partial void OptionalToTranscriptParams_Generated(
        TranscriptOptionalParams optionalParams,
        TranscriptParams transcriptParams
    );

    internal static TranscriptParams ToTranscriptParams(
        this TranscriptOptionalParams optionalParams,
        Uri audioUrl
    ) => ToTranscriptParams(optionalParams, audioUrl.ToString());

    internal static TranscriptParams ToTranscriptParams(
        this TranscriptOptionalParams optionalParams,
        string audioUrl
    )
    {
        var transcriptParams = new TranscriptParams { AudioUrl = audioUrl };
        OptionalToTranscriptParams_Generated(optionalParams, transcriptParams);
        return transcriptParams;
    }
}