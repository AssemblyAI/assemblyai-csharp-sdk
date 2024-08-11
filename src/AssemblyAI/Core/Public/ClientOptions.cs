using System;
using System.Net.Http;

#nullable enable

namespace AssemblyAI;

public partial class ClientOptions
{
    /// <summary>
    /// The AssemblyAI API key
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    /// The Base URL for the API.
    /// </summary>
    public string BaseUrl { get; set; } = AssemblyAIClientEnvironment.Default;

    /// <summary>
    /// The AssemblyAI user agent
    /// </summary>
    public UserAgent UserAgent { get; set; } = new();

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public HttpClient? HttpClient { get; set; }

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public int MaxRetries { get; set; } = 2;

    /// <summary>
    /// The timeout for the request.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
