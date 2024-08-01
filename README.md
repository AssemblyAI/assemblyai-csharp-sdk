# AssemblyAI C# .NET SDK

[![NuGet](https://img.shields.io/nuget/v/AssemblyAI.svg)](https://www.nuget.org/packages/AssemblyAI.net/)
[![GitHub License](https://img.shields.io/github/license/AssemblyAI/assemblyai-csharp-sdk)](https://github.com/AssemblyAI/assemblyai-csharp-sdk/blob/main/LICENSE)
[![AssemblyAI Twitter](https://img.shields.io/twitter/follow/AssemblyAI?label=%40AssemblyAI&style=social)](https://twitter.com/AssemblyAI)
[![AssemblyAI YouTube](https://img.shields.io/youtube/channel/subscribers/UCtatfZMf-8EkIwASXM4ts0A)](https://www.youtube.com/@AssemblyAI)
[![Discord](https://img.shields.io/discord/875120158014853141?logo=discord&label=Discord&link=https%3A%2F%2Fdiscord.com%2Fchannels%2F875120158014853141&style=social)
](https://assemblyai.com/discord)

The AssemblyAI C# SDK provides an easy-to-use interface for interacting with the AssemblyAI API from .NET, which supports async and real-time transcription, as well as the latest audio intelligence and LeMUR models. 
The C# SDK is compatible with `.NET 6.0` and up, `.NET Framework 4.6.2` and up, and `.NET Standard 2.0`.

## Documentation

Visit the [AssemblyAI documentation](https://www.assemblyai.com/docs) for step-by-step instructions and a lot more details about our AI models and API.

## Quickstart

You can find the `AssemblyAI` C# SDK on [NuGet](https://www.nuget.org/packages/AssemblyAI).
Add the latest version using the .NET CLI:

```bash
dotnet add package AssemblyAI --prerelease
```

Then, create an AssemblyAIClient with your API key:

```csharp
using AssemblyAI;

var client = new AssemblyAIClient(Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!);
```

You can now use the `client` object to interact with the AssemblyAI API.

## Add the AssemblyAIClient to the dependency injection container

The AssemblyAI SDK has built-in support for default .NET dependency injection container. 
Add the `AssemblyAIClient` to the service collection like this:

```csharp
using AssemblyAI;

// build your services
services.AddAssemblyAIClient();

```

By default, the `AssemblyAIClient` loads it configuration from the `AssemblyAI` section from the .NET configuration.

```json
{
  "AssemblyAI": {
    "ApiKey": "YOUR_ASSEMBLYAI_API_KEY"
  }
}
```

You can also configure the `AssemblyAIClient` other ways using the `AddAssemblyAIClient` overloads.

```csharp
using AssemblyAI;

// build your services
services.AddAssemblyAIClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!;
});
```

## Speech-To-Text

### Transcribe audio and video files

<details open>
  <summary>Transcribe an audio file with a public URL</summary>

When you create a transcript, you can either pass in a URL to an audio file or upload a file directly.

```csharp
using AssemblyAI;
using AssemblyAI.Transcripts;

var client = new AssemblyAIClient(Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!);

// Transcribe file at remote URL
var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
{
    AudioUrl = "https://storage.googleapis.com/aai-web-samples/espn-bears.m4a",
    LanguageCode = TranscriptLanguageCode.EnUs
});
```

`TranscribeAsync` queues a transcription job and polls it until the `transcript.Status` is `completed` or `error`.

If you don't want to wait until the transcript is ready, you can use `submit`:

```csharp
transcript = await client.Transcripts.SubmitAsync(new TranscriptParams
{
    AudioUrl = "https://storage.googleapis.com/aai-web-samples/espn-bears.m4a",
    LanguageCode = TranscriptLanguageCode.EnUs
});
```

</details>

<details>
  <summary>Transcribe a local audio file</summary>

When you create a transcript, you can either pass in a URL to an audio file or upload a file directly.

```csharp
using AssemblyAI;
using AssemblyAI.Transcripts;

var client = new AssemblyAIClient(Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!);

// Transcribe file using file info
var transcript = await client.Transcripts.TranscribeAsync(
    new FileInfo("./news.mp4"), 
    new TranscriptOptionalParams
    {
        LanguageCode = TranscriptLanguageCode.EnUs
    }
);

// Transcribe file from stream
await using var stream = new FileStream("./news.mp4", FileMode.Open);
transcript = await client.Transcripts.TranscribeAsync(
    stream, 
    new TranscriptOptionalParams
    {
        LanguageCode = TranscriptLanguageCode.EnUs
    }
);
```

`transcribe` queues a transcription job and polls it until the `status` is `completed` or `error`.

If you don't want to wait until the transcript is ready, you can use `submit`:

```csharp
transcript = await client.Transcripts.SubmitAsync(
    new FileInfo("./news.mp4"), 
    new TranscriptOptionalParams
    {
        LanguageCode = TranscriptLanguageCode.EnUs
    }
);
```

</details>

<details>
  <summary>Enable additional AI models</summary>

You can extract even more insights from the audio by enabling any of our [AI models](https://www.assemblyai.com/docs/audio-intelligence) using _transcription options_.
For example, here's how to enable [Speaker diarization](https://www.assemblyai.com/docs/speech-to-text/speaker-diarization) model to detect who said what.

```csharp
var transcript = await client.Transcripts.TranscribeAsync(new TranscriptParams
{
    AudioUrl = "https://storage.googleapis.com/aai-web-samples/espn-bears.m4a",
    SpeakerLabels = true
});

foreach (var utterance in transcript.Utterances)
{
    Console.WriteLine($"Speaker {utterance.Speaker}: {utterance.Text}");
}
```

</details>

<details>
  <summary>Get a transcript</summary>

This will return the transcript object in its current state. If the transcript is still processing, the `Status` field will be `Queued` or `Processing`. Once the transcript is complete, the `Status` field will be `Completed`.

```csharp
var transcript = await client.Transcripts.GetAsync(transcript.Id);
```

If you created a transcript using `.SubmitAsync(...)`, you can still poll until the transcript `Status` is `Completed` or `Error` using `.WaitUntilReady(...)`:

```csharp
transcript = await client.Transcripts.WaitUntilReady(
    transcript.Id,
    pollingInterval: TimeSpan.FromSeconds(1),
    pollingTimeout: TimeSpan.FromMinutes(10)
);
```

</details>
<details>
  <summary>Get sentences and paragraphs</summary>

```csharp
var sentences = await client.Transcripts.GetSentencesAsync(transcript.Id);
var paragraphs = await client.Transcripts.GetParagraphsAsync(transcript.Id);
```

</details>

<details>
  <summary>Get subtitles</summary>

```csharp
const int charsPerCaption = 32;
var srt = await client.Transcripts.GetSubtitlesAsync(transcript.Id, SubtitleFormat.Srt);
srt = await client.Transcripts.GetSubtitlesAsync(transcript.Id, SubtitleFormat.Srt, charsPerCaption: charsPerCaption);

var vtt = await client.Transcripts.GetSubtitlesAsync(transcript.Id, SubtitleFormat.Vtt);
vtt = await client.Transcripts.GetSubtitlesAsync(transcript.Id, SubtitleFormat.Vtt, charsPerCaption: charsPerCaption);
```

</details>
<details>
  <summary>List transcripts</summary>

This will return a page of transcripts you created.

```csharp
var page = await client.Transcripts.ListAsync();
```

You can also paginate over all pages.

```csharp
var page = await client.Transcripts.ListAsync();
while(page.PageDetails.PrevUrl != null)
{
    page = await client.Transcripts.ListAsync(page.PageDetails.PrevUrl);
}
```

> [!NOTE]
> To paginate over all pages, you need to use the `page.PageDetails.PrevUrl`
> because the transcripts are returned in descending order by creation date and time.
> The first page is are the most recent transcript, and each "previous" page are older transcripts.

</details>

<details>
<summary>Delete a transcript</summary>


```csharp
var transcript = await client.Transcripts.DeleteAsync(transcript.Id);
```

</details>

### Transcribe in real-time

Create the real-time transcriber.

```csharp
using AssemblyAI;
using AssemblyAI.Realtime;

var client = new AssemblyAIClient(Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY")!);
await using var transcriber = client.Realtime.Transcriber();
```

You can also pass in the following options.

```csharp
using AssemblyAI;
using AssemblyAI.Realtime;

await using var transcriber = client.Realtime.Transcriber(new RealtimeTranscriberOptions
{
    // If ApiKey is null, the API key passed to `AssemblyAIClient` will be
    ApiKey: Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY"), 
    RealtimeUrl = "wss://localhost/override",
    SampleRate = 16_000,
    WordBoost = new[] { "foo", "bar" }
});
```

> [!WARNING]
> Storing your API key in client-facing applications exposes your API key.
> Generate a temporary auth token on the server and pass it to your client.
> _Server code_:
>
> ```csharp
> var token = await client.Realtime.CreateTemporaryTokenAsync(expiresIn: 60);
> // TODO: return token to client
> ```
> 
> _Client code_:
>
> ```csharp
> using AssemblyAI;
> using AssemblyAI.Realtime;
> 
> var token = await GetToken();
> await using var transcriber = new RealtimeTranscriber {
>    Token = token.Token
> };
> ```

You can configure the following events.

```csharp
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
```

After configuring your events, connect to the server.
```csharp
await transcriber.ConnectAsync();
```

Send audio data via chunks.

```csharp
// Pseudo code for getting audio
GetAudio(audioChunk => {
    transcriber.SendAudio(audioChunk);
});
```

Close the connection when you're finished.

```csharp
await transcriber.CloseAsync();
```

## Apply LLMs to your audio with LeMUR

Call [LeMUR endpoints](https://www.assemblyai.com/docs/api-reference/lemur) to apply LLMs to your transcript.

<details open>
<summary>Prompt your audio with LeMUR</summary>

```csharp
var response = await client.Lemur.TaskAsync(new LemurTaskParams
{
    TranscriptIds = ["0d295578-8c75-421a-885a-2c487f188927"],
    Prompt = "Write a haiku about this conversation.",
});
```

</details>

<details>
<summary>Summarize with LeMUR</summary>

```csharp
var response = await client.Lemur.SummaryAsync(new LemurSummaryParams
{
    TranscriptIds = ["0d295578-8c75-421a-885a-2c487f188927"],
    AnswerFormat = "one sentence",
    Context = new Dictionary<string, object?>
    {
        ["Speaker"] = new[] { "Alex", "Bob" }
    }
});
```

</details>

<details>
<summary>Ask questions</summary>

```csharp
var response = await client.Lemur.QuestionAnswerAsync(new LemurQuestionAnswerParams
{
    TranscriptIds =  ["0d295578-8c75-421a-885a-2c487f188927"],
    Questions = [
        new LemurQuestion
        {
            Question = "What are they discussing?",
            AnswerFormat = "text"
        }
    ]
});
```

</details>
<details>
<summary>Generate action items</summary>

```csharp
var response = await client.Lemur.ActionItemsAsync(new LemurActionItemsParams
{
    TranscriptIds = ["0d295578-8c75-421a-885a-2c487f188927"]
});
```

</details>
<details>
<summary>Delete LeMUR request</summary>

```csharp
var response = await client.Lemur.PurgeRequestDataAsync(lemurResponse.RequestId);
```

</details>
