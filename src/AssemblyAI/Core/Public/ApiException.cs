using System.Text.Json;
using AssemblyAI.Core;

namespace AssemblyAI;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ApiException(string message, int statusCode, object body)
    : AssemblyAIException(GetMessageFromJsonBody(message, body))
{
    /// <summary>
    /// The error code of the response that triggered the exception.
    /// </summary>
    public int StatusCode { get; } = statusCode;

    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    internal object Body { get; } = body;
    
    private static string GetMessageFromJsonBody(string message, object body)
    {
        if (body is not string stringBody) return message;
        try
        {
            var errorObject = JsonUtils.Deserialize<Error>(stringBody);
            return errorObject.Error_;
        }
        catch (JsonException)
        {
            return stringBody;
        }
    }
}