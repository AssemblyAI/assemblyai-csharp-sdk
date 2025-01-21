using AssemblyAI.Core;
using AssemblyAI.Files;
using AssemblyAI.Lemur;
using AssemblyAI.Realtime;
using AssemblyAI.Transcripts;

#nullable enable

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private RawClient _client;

    public AssemblyAIClient(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "Authorization", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "AssemblyAI" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "AssemblyAI/1.2.1" },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
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
