namespace AssemblyAI.Core;

public sealed class ClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, string> _headers;
    private readonly ClientOptions _clientOptions;

    public ClientWrapper(ClientOptions _clientOptions, HttpClient httpClient, Dictionary<string, string> headers)
    {
        _clientOptions = _clientOptions;
        _httpClient = httpClient;
        _headers = headers;
    }

    public HttpClient HttpClient => _httpClient;

    public string BaseUrl => _clientOptions.BaseURL;

    public Dictionary<string, string> Headers => _headers;
}