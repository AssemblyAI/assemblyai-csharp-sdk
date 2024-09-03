using System.Text;
using Microsoft.Extensions.Configuration;
using AssemblyAI.Realtime;

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

await using var transcriber = new RealtimeTranscriber
{
    ApiKey = config["AssemblyAI:ApiKey"]!,
    SampleRate = 16_000,
    WordBoost = ["word1", "word2"],
    Encoding = AudioEncoding.PcmMulaw
};

transcriber.SessionBegins.Subscribe(
    message => Console.WriteLine(
        $"""
         Session begins:
         - Session ID: {message.SessionId}
         - Expires at: {message.ExpiresAt}
         """)
);

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

transcriber.ErrorReceived.Subscribe(
    error => Console.WriteLine("Error: {0}", error)
);
transcriber.Closed.Subscribe(
    closeEvt => Console.WriteLine("Closed: {0} - {1}", closeEvt.Code, closeEvt.Reason)
);

await transcriber.ConnectAsync().ConfigureAwait(false);

// Mock of streaming audio from a microphone
await using var fileStream = File.OpenRead("./gore-short.wav");
var audio = new Memory<byte>(new byte[8192]);
while (await fileStream.ReadAsync(audio).ConfigureAwait(false) > 0)
{
    await transcriber.SendAudioAsync(audio).ConfigureAwait(false);
    await Task.Delay(100).ConfigureAwait(false);
}

await transcriber.CloseAsync().ConfigureAwait(false);