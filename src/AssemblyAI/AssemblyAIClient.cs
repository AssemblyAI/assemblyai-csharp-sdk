using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private RawClient _client;

    public AssemblyAIClient(string apiKey) : this(apiKey, new ClientOptions())
    {
    }

    public AssemblyAIClient(string apiKey, ClientOptions clientOptions)
    {
        _client = new RawClient(
            new Dictionary<string, string>(),
            clientOptions
        );
        clientOptions.HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", apiKey);
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-Language", "C#");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Name", "AssemblyAI");
        clientOptions.HttpClient.DefaultRequestHeaders.Add("X-Fern-SDK-Version", Constants.Version);
        if (clientOptions.UserAgent != null)
        {
            clientOptions.HttpClient.DefaultRequestHeaders.Add("User-Agent",
                clientOptions.UserAgent.ToAssemblyAIUserAgentString());
        }

        Files = new FilesClient(_client);
        Transcripts = new TranscriptsClient(_client);
        Realtime = new RealtimeClient(_client);
        Lemur = new LemurClient(_client);
    }

    public FilesClient Files { get; init; }

    public TranscriptsClient Transcripts { get; init; }

    public RealtimeClient Realtime { get; init; }

    public LemurClient Lemur { get; init; }
}