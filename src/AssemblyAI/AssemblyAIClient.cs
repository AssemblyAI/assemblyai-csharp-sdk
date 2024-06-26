using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private RawClient _client;

    public AssemblyAIClient(string apiKey = null, ClientOptions clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Authorization", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "AssemblyAI" },
                { "X-Fern-SDK-Version", "0.0.1-alpha" },
            },
            clientOptions ?? new ClientOptions()
        );
        Files = new FilesClient(_client);
        Transcripts = new TranscriptsClient(_client);
        Realtime = new RealtimeClient(_client);
        Lemur = new LemurClient(_client);
        Streaming = new StreamingClient(_client);
    }

    public FilesClient Files { get; }

    public TranscriptsClient Transcripts { get; }

    public RealtimeClient Realtime { get; }

    public LemurClient Lemur { get; }

    public StreamingClient Streaming { get; }

    private string GetFromEnvironmentOrThrow(string env, string message)
    {
        var value = System.Environment.GetEnvironmentVariable(env);
        if (value == null)
        {
            throw new Exception(message);
        }
        return value;
    }
}
