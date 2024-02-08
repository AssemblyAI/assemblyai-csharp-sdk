using Microsoft.Extensions.Configuration;
using AssemblyAI.Realtime;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

await using var transcriber = new RealtimeTranscriber
{
    ApiKey = config["AssemblyAI:ApiKey"]!,
    SampleRate = 16_000,
    WordBoost = new[] { "word1", "word2" }
};

transcriber.SessionBegins += (sender, args) => Console.WriteLine($"""
                                                                  Session begins:
                                                                  - Session ID: {args.Result.SessionId}
                                                                  - Expires at: {args.Result.ExpiresAt}
                                                                  """);
transcriber.PartialTranscripts.Subscribe(t => Console.WriteLine("Partial transcript: {0}", t.Text));
//transcriber.PartialTranscriptReceived += (_, args) => Console.WriteLine("Partial transcript: {0}", args.Result.Text);

transcriber.FinalTranscripts.Subscribe(t => Console.WriteLine("Final transcript: {0}", t.Text));
//transcriber.FinalTranscriptReceived += (_, args) => Console.WriteLine("Final transcript: {0}", args.Result.Text);

transcriber.Transcripts.Subscribe(t => Console.WriteLine("Transcript: {0}", t.Text));
//transcriber.TranscriptReceived += (_, args) => Console.WriteLine("Transcript: {0}", args.Result.Text);

transcriber.ErrorReceived += (_, args) => Console.WriteLine("Error: {0}", args.Error);
transcriber.Closed += (_, _) => Console.WriteLine("Closed");

await transcriber.ConnectAsync();

// Mock of streaming audio from a microphone
await using var fileStream = File.OpenRead("./gore-short.wav");
var audio = new byte[8192];
while (fileStream.Read(audio, 0, audio.Length) > 0)
{
    transcriber.SendAudio(audio);
    await Task.Delay(100);
}

await transcriber.CloseAsync();
