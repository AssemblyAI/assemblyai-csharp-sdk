#nullable enable

using System.Net.Http;
using AssemblyAI;
using AssemblyAI.Core;
using AssemblyAI.Files;
using AssemblyAI.Lemur;
using AssemblyAI.Realtime;
using AssemblyAI.Transcripts;


namespace AssemblyAI;

public partial class AssemblyAIClient
{
    public AssemblyAIClient(string apiKey) : this(new ClientOptions
    {
        ApiKey = apiKey
    })
    {
    }

    public AssemblyAIClient(ClientOptions clientOptions)
    {
        if (string.IsNullOrEmpty(clientOptions.ApiKey))
        {
            throw new ArgumentException("AssemblyAI API Key is required.");
        }

        clientOptions.HttpClient ??= new HttpClient();
        var client = new RawClient(
            new Dictionary<string, string>(),
            new Dictionary<string, Func<string>>(),
            clientOptions
        );
        clientOptions.HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", clientOptions.ApiKey);
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-Language", "C#");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Name", "AssemblyAI");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Version", Constants.Version);
        var userAgent = new UserAgent(UserAgent.Default, clientOptions.UserAgent);
        clientOptions.HttpClient.DefaultRequestHeaders.Add(
            "User-Agent",
            userAgent.ToAssemblyAIUserAgentString()
        );

        Files = new FilesClient(client);
        Transcripts = new ExtendedTranscriptsClient(client, this);
        Realtime = new RealtimeClient(client);
        Lemur = new LemurClient(client);
    }

    public FilesClient Files { get; private init; }

    public ExtendedTranscriptsClient Transcripts { get; private init; }

    public RealtimeClient Realtime { get; private init; }

    public LemurClient Lemur { get; private init; }
}