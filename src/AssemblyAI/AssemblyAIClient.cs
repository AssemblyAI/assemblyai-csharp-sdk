using System.Net.Http;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

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
            clientOptions
        );
        clientOptions.HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", clientOptions.ApiKey);
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-Language", "C#");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Name", "AssemblyAI");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Version", Constants.Version);
        if (clientOptions.UserAgent != null)
        {
            clientOptions.HttpClient.DefaultRequestHeaders.Add("User-Agent",
                clientOptions.UserAgent.ToAssemblyAIUserAgentString());
        }

        Files = new FilesClient(client);
        Transcripts = new TranscriptsClient(client);
        Realtime = new RealtimeClient(client);
        Lemur = new LemurClient(client);
    }

    public FilesClient Files { get; init; }

    public TranscriptsClient Transcripts { get; init; }

    public RealtimeClient Realtime { get; init; }

    public LemurClient Lemur { get; init; }
}
