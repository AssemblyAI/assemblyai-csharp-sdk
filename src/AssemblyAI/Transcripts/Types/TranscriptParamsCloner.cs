using Riok.Mapperly.Abstractions;

// ReSharper disable CheckNamespace

namespace AssemblyAI.Transcripts;

[Mapper(UseDeepCloning = true)]
public static partial class TranscriptParamsCloner
{
    /// <summary>
    /// Deep clone TranscriptParams
    /// </summary>
    /// <param name="transcriptParams">The params to clone</param>
    /// <returns>A deep clone of the params</returns>
    public static partial TranscriptParams Clone(this TranscriptParams transcriptParams);
    
    /// <summary>
    /// Deep clone TranscriptOptionalParams
    /// </summary>
    /// <param name="transcriptParams">The params to clone</param>
    /// <returns>A deep clone of the params</returns>
    public static partial TranscriptOptionalParams Clone(this TranscriptOptionalParams transcriptParams);
}