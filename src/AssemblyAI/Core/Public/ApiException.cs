using System.Net;
using System.Text.Json;
using AssemblyAI.Core;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AssemblyAI;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ApiException: AssemblyAIException
{
    /// <summary>
    /// The error code of the response that triggered the exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    internal object Body { get; }
    
    /// <summary>
    /// The content of the response that triggered the exception.
    /// </summary>
    public string ResponseContent { get; }

    internal ApiException(string message, int statusCode, object body)
        : base(GetMessageFromJsonBody(message, body))
    {
        StatusCode = (HttpStatusCode)statusCode;
        Body = body;
        ResponseContent = GetResponseContentFromBody(body);
    }
    
    // TODO: Remove when Fern generator can set this property
    /// <summary>
    /// Serialize the HTTP response body to a string.
    /// </summary>
    /// <param name="body">The HTTP response body</param>
    /// <returns>The serialized HTTP response body</returns>
    private static string GetResponseContentFromBody(object body)
    {
        return body switch
        {
            null => string.Empty,
            string stringBody => stringBody,
            _ => JsonUtils.Serialize(body)
        };
    }
    
    /// <summary>
    /// Retrieves the error message from the JSON body of the response.
    /// </summary>
    /// <param name="message">The default error message to fallback to</param>
    /// <param name="body">The HTTP response body</param>
    /// <returns>The error message</returns>
    private static string GetMessageFromJsonBody(string message, object body)
    {
        if (body is not string stringBody) return message;
        if (stringBody == "") return message;
        try
        {
            var errorObject = JsonUtils.Deserialize<JsonElement>(stringBody);
            if(errorObject.TryGetProperty("error", out var errorProperty))
            {
                return errorProperty.GetString()!;
            }
        }
        catch (JsonException)
        {
        }
        return stringBody;
    }
}