namespace AssemblyAI.Core;

public sealed class ClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, string> _headers;
    private readonly ClientOptions _clientOptions;

    public ClientWrapper(ClientOptions clientOptions, HttpClient httpClient, Dictionary<string, string> headers)
    {
        _clientOptions = clientOptions;
        _httpClient = httpClient;
        foreach (var kv in headers)
        {
            httpClient.DefaultRequestHeaders.Add(kv.Key, kv.Value);
        }
        _headers = headers;
    }

    public HttpClient HttpClient => _httpClient;

    public string BaseUrl => _clientOptions.BaseURL;

    public Dictionary<string, string> Headers => _headers;
}