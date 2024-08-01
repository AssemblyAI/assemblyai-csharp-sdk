using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class UnauthorizedError(Error body)
    : AssemblyAIClientApiException("UnauthorizedError", 401, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new Error Body { get; } = body;
}
