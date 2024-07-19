using System.Net.Http;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private RawClient _client;

    public AssemblyAIClient(string apiKey) : this(new ClientOptions
    {
        ApiKey = apiKey
    })
    {
    }

    public AssemblyAIClient(ClientOptions clientOptions)
    {
        clientOptions.HttpClient ??= new HttpClient();
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Authorization", clientOptions.ApiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "AssemblyAI" },
                { "X-Fern-SDK-Version", "0.0.2-alpha" },
            },
            clientOptions
        );
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