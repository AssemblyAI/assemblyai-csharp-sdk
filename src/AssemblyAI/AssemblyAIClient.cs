using AssemblyAI.Core;
using AssemblyAI.Files;
using AssemblyAI.Realtime;
using AssemblyAI.Lemur;
using AssemblyAI.Transcripts;

namespace AssemblyAI;

public partial class AssemblyAIClient
{
    private readonly ClientWrapper _clientWrapper;
    
    public AssemblyAIClient(string apiKey) : this(apiKey, new ClientOptions()) {}
    
    public AssemblyAIClient(string apiKey, ClientOptions clientOptions)
    {
            
            _clientWrapper = new ClientWrapper(clientOptions, clientOptions.HttpClient, new Dictionary<string, string>()
            {
                { "Authorization", apiKey },
                { "X-Fern-SDK-Name", "assemblyai" },
                { "X-Fern-SDK-Version", "0.0.1-beta0" },
                { "X-Fern-SDK-Language", "dotnet" },
            });
            Files = new FilesClient(_clientWrapper);
            Transcripts = new TranscriptsClient(_clientWrapper);
            Lemur = new LemurClient(_clientWrapper);
            Realtime = new RealtimeClient(_clientWrapper);
        }
    
    public FilesClient Files { get; }
    public TranscriptsClient Transcripts { get; }
    public LemurClient Lemur { get; }
    
    public RealtimeClient Realtime { get; }
}