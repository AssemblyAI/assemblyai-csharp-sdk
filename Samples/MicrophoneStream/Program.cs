using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using AssemblyAI.Realtime;
using Microsoft.Extensions.Configuration;

var transcriptTexts = new SortedDictionary<int, string>();

string BuildTranscript()
{
    var stringBuilder = new StringBuilder();
    foreach (var word in transcriptTexts.Values)
    {
        stringBuilder.Append($"{word} ");
    }

    return stringBuilder.ToString();
}

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Set up the cancellation token, so we can stop the program with Ctrl+C
var cts = new CancellationTokenSource();
var ct = cts.Token;
Console.CancelKeyPress += (sender, e) => cts.Cancel();

// Set up the realtime transcriber
const int sampleRate = 16_000;
await using var transcriber = new RealtimeTranscriber(new RealtimeTranscriberOptions
{
    ApiKey = config["AssemblyAI:ApiKey"]!,
    SampleRate = sampleRate
});

transcriber.PartialTranscriptReceived.Subscribe(transcript =>
{
    // don't do anything if nothing was said
    if (string.IsNullOrEmpty(transcript.Text)) return;
    transcriptTexts[transcript.AudioStart] = transcript.Text;

    Console.Clear();
    Console.WriteLine(BuildTranscript());
});
transcriber.FinalTranscriptReceived.Subscribe(transcript =>
{
    transcriptTexts[transcript.AudioStart] = transcript.Text;

    Console.Clear();
    Console.WriteLine(BuildTranscript());
});
transcriber.ErrorReceived.Subscribe(error => Console.WriteLine("Real-time error: {0}", error));
transcriber.Closed.Subscribe(closeEvent =>
    Console.WriteLine("Real-time connection closed: {0} - {1}",
        closeEvent.Code,
        closeEvent.Reason
    )
);

Console.WriteLine("Connecting to real-time transcript service");
var sessionBeginsMessage = await transcriber.ConnectAsync().ConfigureAwait(false);
Console.WriteLine($"""
                   Session begins:
                   - Session ID: {sessionBeginsMessage.SessionId}
                   - Expires at: {sessionBeginsMessage.ExpiresAt}
                   """);
Console.WriteLine("Starting recording");

var soxArguments = string.Join(' ', [
    // --default-device doesn't work on Windows
    RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "-t waveaudio default" : "--default-device",
    "--no-show-progress",
    "--rate 16000",
    "--channels 1",
    "--encoding signed-integer",
    "--bits 16",
    "--type wav",
    "-" // pipe
]);
Console.WriteLine($"sox {soxArguments}");
using var soxProcess = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "sox",
        Arguments = soxArguments,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    }
};

soxProcess.Start();
soxProcess.BeginErrorReadLine();
var soxOutputStream = soxProcess.StandardOutput.BaseStream;
var buffer = new byte[4096];
while (await soxOutputStream.ReadAsync(buffer, 0, buffer.Length, ct) > 0)
{
    if (ct.IsCancellationRequested) break;
    await transcriber.SendAudioAsync(buffer);
}

soxProcess.Kill();
await transcriber.CloseAsync();