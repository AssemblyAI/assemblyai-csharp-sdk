using Riok.Mapperly.Abstractions;

// ReSharper disable CheckNamespace

namespace AssemblyAI.Transcripts;

[Mapper(UseDeepCloning = true)]
internal static partial class TranscriptParamsCloner
{
    public static partial TranscriptParams Clone(this TranscriptParams transcriptParams);
    
    public static partial TranscriptOptionalParams Clone(this TranscriptOptionalParams transcriptParams);
}