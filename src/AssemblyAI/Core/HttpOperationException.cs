using System.Net;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Core;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class HttpOperationException(string message, HttpStatusCode statusCode, string responseContent)
    : AssemblyAIException(message)
{
    /// <summary>
    /// The error code of the response that triggered the exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public string ResponseContent { get; } = responseContent;
}
