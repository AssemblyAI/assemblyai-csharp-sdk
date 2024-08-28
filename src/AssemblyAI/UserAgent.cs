using AssemblyAI.Core;
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace AssemblyAI;

/// <summary>
/// The AssemblyAI user agent.
/// </summary>
public class UserAgent
{
    /// <summary>
    /// The default AssemblyAI user agent.
    /// </summary>
    public static readonly UserAgent Default = CreateDefaultUserAgent();
    private readonly Dictionary<string, UserAgentItem?> _userAgent;

    /// <summary>
    /// Create a new instance of the <see cref="UserAgent"/> class.
    /// </summary>
    public UserAgent() : this(new Dictionary<string, UserAgentItem?>())
    {
    }

    /// <summary>
    /// Create a new instance of the <see cref="UserAgent"/> class from a dictionary.
    /// </summary>
    /// <param name="userAgent"></param>
    public UserAgent(Dictionary<string, UserAgentItem?> userAgent)
    {
        _userAgent = userAgent;
    }

    /// <summary>
    /// Create a new instance of the <see cref="UserAgent"/> class by merging two user agents.
    /// </summary>
    /// <param name="a">User agent A</param>
    /// <param name="b">User agent B</param>
    public UserAgent(UserAgent a, UserAgent b)
    {
        _userAgent = Merge(a._userAgent, b._userAgent) as Dictionary<string, UserAgentItem?>;
    }
    
    /// <summary>
    /// Get or set a user agent item by key.
    /// </summary>
    /// <param name="index">The name of the user agent item</param>
    public UserAgentItem? this[string index]
    {
        get => _userAgent[index];
        set => _userAgent[index] = value;
    }

    /// <summary>
    /// Convert the AssemblyAI user agent to a string.
    /// </summary>
    /// <returns>The AssemblyAI user agent section</returns>
    public string ToAssemblyAIUserAgentString()
    {
        var sb = new System.Text.StringBuilder("AssemblyAI/1.0 (");
        sb.Append(string.Join(" ",
            _userAgent.Select(entry => $"{entry.Key}={entry.Value!.Name}/{entry.Value.Version}")));
        sb.Append(')');
        return sb.ToString();
    }

    /// <summary>
    /// Create the default AssemblyAI user agent.
    /// </summary>
    /// <returns></returns>
    private static UserAgent CreateDefaultUserAgent()
    {
        var defaultUserAgent = new Dictionary<string, UserAgentItem?>();
        defaultUserAgent["sdk"] = new UserAgentItem("CSharp", CustomConstants.Version);
#if NET462_OR_GREATER
            defaultUserAgent["runtime_env"] = new UserAgentItem(".NET Framework", $"{Environment.Version}");
#else
        var (name, version) =
            ParseFrameworkDescription(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
        defaultUserAgent["runtime_env"] = new UserAgentItem(name, version);
#endif

        return new UserAgent(defaultUserAgent);
    }
    
#if NET462_OR_GREATER
#else
    /// <summary>
    /// Parse the framework description into a name and version.
    /// </summary>
    /// <param name="frameworkDescription"></param>
    /// <returns></returns>
    private static (string Name, string Version) ParseFrameworkDescription(string frameworkDescription)
    {
        string name;
        string version;

        if (frameworkDescription.StartsWith(".NET Framework"))
        {
            name = ".NET Framework";
            version = frameworkDescription.Replace(".NET Framework ", "");
        }
        else if (frameworkDescription.StartsWith(".NET Core"))
        {
            name = ".NET Core";
            version = frameworkDescription.Replace(".NET Core ", "");
        }
        else if (frameworkDescription.StartsWith(".NET Native"))
        {
            name = ".NET Native";
            version = frameworkDescription.Replace(".NET Native ", "");
        }
        else if (frameworkDescription.StartsWith(".NET "))
        {
            name = ".NET";
            version = frameworkDescription.Replace(".NET ", "");
        }
        else
        {
            name = "Unknown";
            version = frameworkDescription;
        }

        return (name, version);
    }
#endif

    /// <summary>
    /// Merge two user agents dictionaries.
    /// </summary>
    /// <param name="a">User agent dictionary A</param>
    /// <param name="b">User agent dictionary B</param>
    /// <returns></returns>
    private static Dictionary<string, UserAgentItem> Merge(
        Dictionary<string, UserAgentItem?> a,
        Dictionary<string, UserAgentItem?> b
    )
    {
        var newUserAgent = new Dictionary<string, UserAgentItem?>(a);

        foreach (var entry in b)
        {
            newUserAgent[entry.Key] = entry.Value;
        }

        // Remove all null values
        var keysToRemove = newUserAgent.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList();
        foreach (var key in keysToRemove)
        {
            newUserAgent.Remove(key);
        }

        return newUserAgent as Dictionary<string, UserAgentItem>;
    }
}

/// <summary>
/// An item in the AssemblyAI user agent.
/// </summary>
/// <param name="name">The user agent item name</param>
/// <param name="version">The user agent item version</param>
public class UserAgentItem(string name, string version)
{
    public string Name { get; set; } = name;
    public string Version { get; set; } = version;
}