namespace AssemblyAI.Core;

public sealed class Environment
{
    public static readonly Environment Production = new Environment("https://api.assemblyai.com");
    
    private readonly string _url;
    
    private Environment(string url)
    {
        this._url = url;
    }

    public string Url => _url;
}