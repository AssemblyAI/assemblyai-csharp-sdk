// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CheckNamespace

namespace AssemblyAI;

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
}