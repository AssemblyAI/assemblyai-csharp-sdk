// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CheckNamespace

using AssemblyAI.Core;

namespace AssemblyAI;

/// <summary>
/// The options for the AssemblyAI client.
/// </summary>
public partial class ClientOptions
{
    /// <summary>
    /// The AssemblyAI API key
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    /// The AssemblyAI user agent
    /// </summary>
    public UserAgent UserAgent { get; set; } = new();

    /// <summary>
    /// Clones this and returns a new instance
    /// </summary>
    internal ClientOptions Clone()
    {
        return new ClientOptions
        {
            ApiKey = ApiKey,
            UserAgent = UserAgent.Clone(),
            BaseUrl = BaseUrl,
            HttpClient = HttpClient,
            MaxRetries = MaxRetries,
            Timeout = Timeout,
            Headers = new Headers(new Dictionary<string, HeaderValue>(Headers)),
        };
    }
}