namespace AssemblyAI.Core;

public sealed class ClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly IReadOnlyDictionary<string, string> _headers;
    private readonly ClientOptions _clientOptions;

    public ClientWrapper(ClientOptions clientOptions, HttpClient httpClient,
        IReadOnlyDictionary<string, string> headers)
    {
        _clientOptions = clientOptions;
        _httpClient = httpClient;
        foreach (var header in headers)
        {
            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }

    public HttpClient HttpClient => _httpClient;

    public string BaseUrl => _clientOptions.BaseUrl;
}