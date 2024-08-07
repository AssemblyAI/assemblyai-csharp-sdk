using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AssemblyAI.Core;

#nullable enable

/// <summary>
/// Utility class for making raw HTTP requests to the API.
/// </summary>
public class RawClient(
    Dictionary<string, string> headers,
    Dictionary<string, Func<string>> headerSuppliers,
    ClientOptions clientOptions
)
{
    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public readonly ClientOptions Options = clientOptions;

    /// <summary>
    /// Global headers to be sent with every request.
    /// </summary>
    private readonly Dictionary<string, string> _headers = headers;

    public async Task<ApiResponse> MakeRequestAsync(BaseApiRequest request)
    {
        var url = BuildUrl(request);
        var httpRequest = new HttpRequestMessage(request.Method, url);
        if (request.ContentType != null)
        {
            request.Headers.Add("Content-Type", request.ContentType);
        }

        // Add global headers to the request
        foreach (var header in _headers)
        {
            httpRequest.Headers.Add(header.Key, header.Value);
        }

        // Add global headers to the request from supplier
        foreach (var header in headerSuppliers)
        {
            httpRequest.Headers.Add(header.Key, header.Value.Invoke());
        }

        // Add request headers to the request
        foreach (var header in request.Headers)
        {
            httpRequest.Headers.Add(header.Key, header.Value);
        }

        // Add the request body to the request
        if (request is JsonApiRequest jsonRequest)
        {
            if (jsonRequest.Body != null)
            {
                httpRequest.Content = new StringContent(
                    JsonUtils.Serialize(jsonRequest.Body),
                    Encoding.UTF8,
                    "application/json"
                );
            }
        }
        else if (request is StreamApiRequest { Body: not null } streamRequest)
        {
            httpRequest.Content = new StreamContent(streamRequest.Body);
        }

        // Send the request
        var httpClient = request.Options?.HttpClient ?? Options.HttpClient
            ?? throw new Exception("No HttpClient provided in request or client options");
        var response = await httpClient.SendAsync(httpRequest);
        var apiResponse = new ApiResponse { StatusCode = response.StatusCode, Raw = response };
        if (response.IsSuccessStatusCode) return apiResponse;
        
        var responseContentString = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContentString))
        {
            throw new HttpOperationException(
                $"Error with status code {response.StatusCode}", 
                response.StatusCode, 
                responseContentString
            ); 
        }

        try
        {
            var error = JsonUtils.Deserialize<AssemblyAI.Error>(responseContentString);
            throw new HttpOperationException(
                error.Error_, 
                response.StatusCode, 
                responseContentString
            );
        }
        catch (JsonException)
        {
        }
        
        // use response content as error message if error object cannot be deserialized
        throw new HttpOperationException(
            responseContentString, 
            response.StatusCode, 
            responseContentString
        );
    }

    public record BaseApiRequest
    {
        public required string BaseUrl { get; init; }

        public required HttpMethod Method { get; init; }

        public required string Path { get; init; }

        public string? ContentType { get; init; }

        public Dictionary<string, object> Query { get; init; } = new();

        public Dictionary<string, string> Headers { get; init; } = new();

        public RequestOptions? Options { get; init; }
    }

    /// <summary>
    /// The request object to be sent for streaming uploads.
    /// </summary>
    public record StreamApiRequest : BaseApiRequest
    {
        public Stream? Body { get; init; }
    }

    /// <summary>
    /// The request object to be sent for JSON APIs.
    /// </summary>
    public record JsonApiRequest : BaseApiRequest
    {
        public object? Body { get; init; }
    }

    /// <summary>
    /// The response object returned from the API.
    /// </summary>
    public record ApiResponse
    {
        public required HttpStatusCode StatusCode { get; init; }

        public required HttpResponseMessage Raw { get; init; }
    }

    private string BuildUrl(BaseApiRequest request)
    {
        var baseUrl = request.Options?.BaseUrl ?? request.BaseUrl;
        var trimmedBaseUrl = baseUrl.TrimEnd('/');
        var trimmedBasePath = request.Path.TrimStart('/');
        var url = $"{trimmedBaseUrl}/{trimmedBasePath}";
        if (request.Query.Count <= 0)
            return url;
        url += "?";
        url = request.Query.Aggregate(
            url,
            (current, queryItem) =>
            {
                if (queryItem.Value is System.Collections.IEnumerable collection and not string)
                {
                    var items = collection
                        .Cast<object>()
                        .Select(value => $"{queryItem.Key}={value}")
                        .ToList();
                    if (items.Any())
                    {
                        current += string.Join("&", items) + "&";
                    }
                }
                else
                {
                    current += $"{queryItem.Key}={queryItem.Value}&";
                }

                return current;
            }
        );
        url = url.Substring(0, url.Length - 1);
        return url;
    }
}