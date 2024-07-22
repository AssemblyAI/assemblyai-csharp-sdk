using System.Net.Http;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Core;

public partial class ClientOptions
{
    /// <summary>
    /// The Base URL for the API.
    /// </summary>
    public string BaseUrl { get; init; } = Environments.DEFAULT;

    private UserAgent? _userAgent = UserAgent.Default;
    
    /// <summary>
    /// The AssemblyAI user agent
    /// </summary>
    public UserAgent? UserAgent
    {
        get => _userAgent;
        set
        {
            if (value == null)
            {
                _userAgent = null;
                return;
            }
            _userAgent = new UserAgent(UserAgent.Default, value);
        }
    }

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public HttpClient HttpClient { get; init; } = new HttpClient();

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public int MaxRetries { get; init; } = 2;

    /// <summary>
    /// The timeout for the request in seconds.
    /// </summary>
    public int TimeoutInSeconds { get; init; } = 30;
}