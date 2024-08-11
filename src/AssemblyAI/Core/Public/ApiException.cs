namespace AssemblyAI;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ApiException(string message, int statusCode, object body)
    : AssemblyAIException(body is Error error ? error.Error_ : message)
{
    /// <summary>
    /// The error code of the response that triggered the exception.
    /// </summary>
    public int StatusCode { get; } = statusCode;

    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    internal object Body { get; } = body;
}
