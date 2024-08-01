using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ServiceUnavailableError(object body)
    : AssemblyAIClientApiException("ServiceUnavailableError", 503, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new object Body { get; } = body;
}
