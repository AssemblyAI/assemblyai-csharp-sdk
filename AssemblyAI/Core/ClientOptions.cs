namespace AssemblyAI.Core;

public class ClientOptions
{
    public HttpClient HttpClient { get; init;} = new HttpClient();
    
    public int MaxRetries { get; init; } = 2;

    public int TimeoutInSeconds { get; init; } = 60;

    public IReadOnlyDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
    public string BaseUrl { get; init; } = Environment.Production.Url;
}