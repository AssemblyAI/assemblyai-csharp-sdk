namespace AssemblyAI.Core;

public sealed class ClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly IReadOnlyDictionary<string, string> _headers;
    private readonly ClientOptions _clientOptions;

    public ClientWrapper(ClientOptions _clientOptions, HttpClient httpClient, IReadOnlyDictionary<string, string> headers)
    {
        _clientOptions = _clientOptions;
        _httpClient = httpClient;
        _headers = headers;
    }

    public HttpClient HttpClient => _httpClient;

    public string BaseUrl => _clientOptions.BaseUrl;

    public IReadOnlyDictionary<string, string> Headers => _headers;
}