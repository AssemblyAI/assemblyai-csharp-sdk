using System.Net.Http;
using AssemblyAI.Core;
using AssemblyAI.Files;
using AssemblyAI.Lemur;
using AssemblyAI.Realtime;
using AssemblyAI.Transcripts;


namespace AssemblyAI;

/// <summary>
/// The client to interact with the AssemblyAI API.
/// </summary>
public class AssemblyAIClient
{
    /// <inheritdoc cref="FilesClient"/>
    public FilesClient Files { get; private init; }

    /// <inheritdoc cref="ExtendedTranscriptsClient"/>
    public ExtendedTranscriptsClient Transcripts { get; private init; }

    /// <inheritdoc cref="RealtimeClient"/>
    public RealtimeClient Realtime { get; private init; }

    /// <inheritdoc cref="LemurClient"/>
    public LemurClient Lemur { get; private init; }
    
    /// <summary>
    /// Create a new instance of the <see cref="AssemblyAIClient"/> class.
    /// </summary>
    /// <param name="apiKey">Your AssemblyAI API key</param>
    /// <exception cref="ArgumentException">Thrown if apiKey is null or empty.</exception>
    public AssemblyAIClient(string apiKey) : this(new ClientOptions
    {
        ApiKey = apiKey
    })
    {
    }

    /// <summary>
    /// Create a new instance of the <see cref="AssemblyAIClient"/> class.
    /// </summary>
    /// <param name="clientOptions">The AssemblyAI client options</param>
    /// <exception cref="ArgumentException">Thrown if ClientOptions.ApiKey is null or empty.</exception>
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