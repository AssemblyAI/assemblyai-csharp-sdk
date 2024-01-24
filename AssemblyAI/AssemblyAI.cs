using AssemblyAI.Core;

namespace AssemblyAI;

public sealed class AssemblyAI
{
    private readonly ClientWrapper _clientWrapper;
    
    public AssemblyAI(string apiKey) : this(apiKey, new ClientOptions()) {}
    
    public AssemblyAI(string apiKey, ClientOptions clientOptions)
    {
        _clientWrapper = new ClientWrapper(clientOptions, clientOptions.HttpClient, new Dictionary<string, string>()
        {
            { "Authorization", apiKey },
            { "X-Fern-SDK-Name", "assemblyai" },
            { "X-Fern-SDK-Version", "0.0.1-beta0" },
            { "X-Fern-SDK-Langauge", "C#" },
        });
        Files = new FilesClient(_clientWrapper);
        Transcript = new TranscriptClient(_clientWrapper);
        Lemur = new LemurClient(_clientWrapper);
        Realtime = new RealtimeClient(_clientWrapper);
    }
    
    public FilesClient Files { get; }
    public TranscriptClient Transcript { get; }
    public LemurClient Lemur { get; }
    
    public RealtimeClient Realtime { get; }
}