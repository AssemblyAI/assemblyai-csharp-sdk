namespace AssemblyAI.Core;

public sealed class URLBuilder
{

    private readonly string _baseUrl;
    private readonly List<string> _pathSegments;
    private readonly Dictionary<string, List<string>> _queryParameters;

    public URLBuilder(string baseUrl)
    {
        this._baseUrl = baseUrl;
    }
    
    /**
     * Add a path segment to the URL.
     * AddPathSegment("/bar") adds `/bar` to the url.
     * AddPathSegment("/bar/baz/foo") adds `/bar/baz/foo` to the url.
     */
    public URLBuilder AddPathSegment(string path)
    {
        if (path.StartsWith("/"))
        {
            path = path.Substring(1);
        }
        _pathSegments.Add(path);
        return this;
    }
    
    /**
     * Add a query parameter ot the URL.
     * AddQueryParameter("bar", "baz") will append `?bar=baz` to the URL.
     * If multiple query parameters are 
     */
    public URLBuilder AddQueryParameter(string key, string value)
    {
        List<string> values;
        if (!_queryParameters.TryGetValue(key, out values)) {
            values = new List<string>();
            _queryParameters[key] = values;
        }
        values.Add(value);
        return this;
    }
    
    /**
     * Builds and returns full URL;
     */
    public string build()
    {
        string url = this._baseUrl;
        if (_pathSegments.Count > 0)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            url += string.Join("/", _pathSegments);
        }
        List<string> queryParamEntries = getQueryParamEntries();
        if (queryParamEntries.Count > 0)
        {
            url += "?" + string.Join("&", queryParamEntries);
        }
        return url;
    }

    private List<string> getQueryParamEntries()
    {
        List<string> queryParamEntries = new List<string>();
        foreach(KeyValuePair<string, List<string>> entry in _queryParameters)
        {
            foreach (var value in entry.Value)
            {
                queryParamEntries.Add(entry.Key + "=" + value);
            }
        }
        return queryParamEntries;
    }
}