using AssemblyAI.Core;
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace AssemblyAI;

public class UserAgent
{
    public static readonly UserAgent Default = CreateDefaultUserAgent();
    private readonly Dictionary<string, UserAgentItem?> _userAgent;

    public UserAgent() : this(new Dictionary<string, UserAgentItem?>())
    {
    }

    public UserAgent(Dictionary<string, UserAgentItem?> userAgent)
    {
        _userAgent = userAgent;
    }

    public UserAgent(UserAgent a, UserAgent b)
    {
        _userAgent = Merge(a._userAgent, b._userAgent) as Dictionary<string, UserAgentItem?>;
    }
    
    public UserAgentItem? this[string index]
    {
        get => _userAgent[index];
        set => _userAgent[index] = value;
    }

    public string ToAssemblyAIUserAgentString()
    {
        var sb = new System.Text.StringBuilder("AssemblyAI/1.0 (");
        sb.Append(string.Join(" ",
            _userAgent.Select(entry => $"{entry.Key}={entry.Value!.Name}/{entry.Value.Version}")));
        sb.Append(')');
        return sb.ToString();
    }

    private static UserAgent CreateDefaultUserAgent()
    {
        var defaultUserAgent = new Dictionary<string, UserAgentItem?>();
        defaultUserAgent["sdk"] = new UserAgentItem("CSharp", Constants.Version);
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

public class UserAgentItem(string name, string version)
{
    public string Name { get; set; } = name;
    public string Version { get; set; } = version;
}