using System;
using System.Net.Http;
using AssemblyAI.Core;

#nullable enable

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
    public HttpClient HttpClient { get; set; } = new HttpClient();

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
}
