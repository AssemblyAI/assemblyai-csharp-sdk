using AssemblyAI;
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

transcriber.SessionBegins.Subscribe(
    message => Console.WriteLine(
        $"""
         Session begins:
         - Session ID: {message.SessionId}
         - Expires at: {message.ExpiresAt}
         """)
);

transcriber.PartialTranscriptReceived.Subscribe(
    transcript => Console.WriteLine("Partial transcript: {0}", transcript.Text)
);

transcriber.FinalTranscriptReceived.Subscribe(
    transcript => Console.WriteLine("Final transcript: {0}", transcript.Text)
);

transcriber.TranscriptReceived.Subscribe(
    transcript => Console.WriteLine("Transcript: {0}", transcript.Match(
        partialTranscript => partialTranscript.Text,
        finalTranscript => finalTranscript.Text
    ))
);

transcriber.ErrorReceived.Subscribe(
    error => Console.WriteLine("Error: {0}", error)
);
transcriber.Closed.Subscribe(
    closeEvt => Console.WriteLine("Closed: {0} - {1}", closeEvt.Code, closeEvt.Reason)
);

await transcriber.ConnectAsync().ConfigureAwait(false);

// Mock of streaming audio from a microphone
await using var fileStream = File.OpenRead("./gore-short.wav");
var audio = new byte[8192];
while (fileStream.Read(audio, 0, audio.Length) > 0)
{
    transcriber.SendAudio(audio);
    await Task.Delay(100);
}

await transcriber.CloseAsync().ConfigureAwait(false);