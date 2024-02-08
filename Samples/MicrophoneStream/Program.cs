using System.Text;
using AssemblyAI.Realtime;
using Microsoft.Extensions.Configuration;
using OpenTK.Audio.OpenAL;

var transcriptWords = new SortedDictionary<int, string>();
string BuildTranscript()
{
    var stringBuilder = new StringBuilder();
    foreach (var word in transcriptWords.Values)
    {
        stringBuilder.Append($"{word} ");
    }

    return stringBuilder.ToString();
}

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var cts = new CancellationTokenSource();
var ct = cts.Token;
var transcribeTask = Task.Run(async () =>
{
    const int sampleRate = 16_000;
    await using var transcriber = new RealtimeTranscriber
    {
        ApiKey = config["AssemblyAI:ApiKey"]!,
        SampleRate = sampleRate
    };
    transcriber.SessionBegins += (sender, args) => Console.WriteLine($"""
                                                                      Session begins:
                                                                      - Session ID: {args.Result.SessionId}
                                                                      - Expires at: {args.Result.ExpiresAt}
                                                                      """);
    transcriber.PartialTranscriptReceived += (_, args) =>
    {
        // don't do anything if nothing was said
        if (string.IsNullOrEmpty(args.Result.Text)) return;
        foreach (var word in args.Result.Words)
        {
            transcriptWords[word.Start] = word.Text;
        }

        Console.Clear();
        Console.WriteLine("Press any key to exit.");
        Console.WriteLine(BuildTranscript());
    };
    transcriber.FinalTranscriptReceived += (_, args) =>
    {
        foreach (var word in args.Result.Words)
        {
            transcriptWords[word.Start] = word.Text;
        }

        Console.Clear();
        Console.WriteLine("Press any key to exit.");
        Console.WriteLine(BuildTranscript());
    };
    
    transcriber.ErrorReceived += (_, args) => Console.WriteLine("Real-time error: {0}", args.Error);
    transcriber.Closed += (_, args) =>
        Console.WriteLine("Real-time connection closed: {0} - {1}", args.Code, args.Reason);

    Console.WriteLine("Connecting to real-time transcript service");
    await transcriber.ConnectAsync();

    Console.WriteLine("Starting recording");

    const int bufferSize = 1024;
    var captureDevice = ALC.CaptureOpenDevice(null, sampleRate, ALFormat.Mono16, bufferSize);
    ALC.CaptureStart(captureDevice);

    var buffer = new byte[bufferSize];
    while (true)
    {
        var current = 0;
        while (current < buffer.Length && !ct.IsCancellationRequested)
        {
            var samplesAvailable = ALC.GetInteger(captureDevice, AlcGetInteger.CaptureSamples);
            if (samplesAvailable < 512) continue;
            var samplesToRead = Math.Min(samplesAvailable, buffer.Length - current);
            ALC.CaptureSamples(captureDevice, ref buffer[current], samplesToRead);
            current += samplesToRead;
        }

        if (ct.IsCancellationRequested) break;

        transcriber.SendAudio(buffer.ToArray());
    }

    Console.WriteLine("Stopping recording");
    ALC.CaptureStop(captureDevice);

    Console.WriteLine("Closing real-time transcript connection");
    await transcriber.CloseAsync();
});

var listenForKeyTask = Task.Run(async () =>
{
    Console.ReadKey();
    await cts.CancelAsync().ConfigureAwait(false);
});

await Task.WhenAny(transcribeTask, listenForKeyTask);
