namespace AssemblyAI.Core;

public sealed class Environment
{
    public static readonly Environment Default = new Environment("https://api.assemblyai.com");
    public static readonly Environment Sandbox = new Environment("https://sandbox.assemblyai.com");

    private readonly string _url = "https://api.assemblyai.com";

    private Environment(string url)
    {
        this._url = url;
    }

    public string URL => _url;
}