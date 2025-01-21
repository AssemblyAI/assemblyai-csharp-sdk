using System.Net.Http;
using AssemblyAI.Core;
// ReSharper disable CheckNamespace

namespace AssemblyAI;

public partial class ClientOptions
{
    /// <summary>
    /// The Base URL for the API.
    /// </summary>
    public string BaseUrl { get; set; } = AssemblyAIClientEnvironment.Default;

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public HttpClient HttpClient { get; set; } = new();

    /// <summary>
    /// The amount to retry sending the HTTP request if it fails.
    /// </summary>
    public int MaxRetries { get; set; } = 2;

    /// <summary>
    /// The timeout for the request.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// The http headers sent with the request.
    /// </summary>
    internal Headers Headers { get; init; } = new();

    /// <summary>
    /// Clones this and returns a new instance
    /// </summary>
    internal ClientOptions Clone()
    {
        return new ClientOptions
        {
            BaseUrl = BaseUrl,
            HttpClient = HttpClient,
            MaxRetries = MaxRetries,
            Timeout = Timeout,
            Headers = new Headers(new Dictionary<string, HeaderValue>(Headers)),
        };
    }
}
