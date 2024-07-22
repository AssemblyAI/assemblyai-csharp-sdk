using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private RawClient _client;

    public AssemblyAIClient(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Authorization", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "AssemblyAI" },
                { "X-Fern-SDK-Version", "0.0.2-alpha" },
            },
            clientOptions ?? new ClientOptions()
        );
        Files = new FilesClient(_client);
        Transcripts = new ExtendedTranscriptsClient(_client, this);
        Realtime = new RealtimeClient(_client);
        Lemur = new LemurClient(_client);
    }

    public FilesClient Files { get; init; }

    public ExtendedTranscriptsClient Transcripts { get; init; }

    public RealtimeClient Realtime { get; init; }

    public LemurClient Lemur { get; init; }
}
