#nullable enable

using System.Net.Http;
using AssemblyAI.Core;
using AssemblyAI.Files;
using AssemblyAI.Lemur;
using AssemblyAI.Realtime;
using AssemblyAI.Transcripts;


namespace AssemblyAI;

public partial class AssemblyAIClient
{

    public FilesClient Files { get; private init; }

    public ExtendedTranscriptsClient Transcripts { get; private init; }

    public RealtimeClient Realtime { get; private init; }

    public LemurClient Lemur { get; private init; }
    
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
        
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        clientOptions.HttpClient ??= new HttpClient();
        clientOptions.Headers.Add("Authorization", clientOptions.ApiKey);
        clientOptions.Headers.Add("User-Agent", new UserAgent(UserAgent.Default, clientOptions.UserAgent).ToAssemblyAIUserAgentString());
        var client = new RawClient(clientOptions);

        Files = new FilesClient(client);
        Transcripts = new ExtendedTranscriptsClient(client, this);
        Realtime = new RealtimeClient(client);
        Lemur = new LemurClient(client);
    }
}