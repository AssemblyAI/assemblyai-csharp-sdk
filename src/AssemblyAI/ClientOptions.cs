#nullable enable

using System.Net.Http;
using AssemblyAI.Core;

namespace AssemblyAI;

public class ClientOptions
{
    /// <summary>
    /// The AssemblyAI API key
    /// </summary>
    public required string ApiKey { get; set; }
    
    /// <summary>
    /// The Base URL for the API.
    /// </summary>
    public string BaseUrl { get; set; } = AssemblyAIClientEnvironment.DEFAULT;

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
    public HttpClient? HttpClient { get; set; }

    /// <summary>
    /// The http client used to make requests.
    /// </summary>
    public int MaxRetries { get; set; } = 2;

    /// <summary>
    /// The timeout for the request in seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
