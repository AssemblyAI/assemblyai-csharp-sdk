# Reference
## Files
## Transcripts
<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">ListAsync</a>(ListTranscriptParams { ... }) -> TranscriptList</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of transcripts you created.
Transcripts are sorted from newest to oldest. The previous URL always points to a page with older transcripts.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.ListAsync(new ListTranscriptParams());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListTranscriptParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">SubmitAsync</a>(TranscriptParams { ... }) -> Transcript</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a transcript from a media file that is accessible via a URL.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.SubmitAsync(
    new TranscriptParams
    {
        LanguageCode = TranscriptLanguageCode.EnUs,
        LanguageDetection = true,
        LanguageConfidenceThreshold = 0.7f,
        Punctuate = true,
        FormatText = true,
        Disfluencies = false,
        Multichannel = true,
        DualChannel = false,
        WebhookUrl = "https://your-webhook-url/path",
        WebhookAuthHeaderName = "webhook-secret",
        WebhookAuthHeaderValue = "webhook-secret-value",
        AutoHighlights = true,
        AudioStartFrom = 10,
        AudioEndAt = 280,
        WordBoost = new List<string>() { "aws", "azure", "google cloud" },
        BoostParam = TranscriptBoostParam.High,
        FilterProfanity = true,
        RedactPii = true,
        RedactPiiAudio = true,
        RedactPiiAudioQuality = RedactPiiAudioQuality.Mp3,
        RedactPiiPolicies = new List<PiiPolicy>()
        {
            PiiPolicy.UsSocialSecurityNumber,
            PiiPolicy.CreditCardNumber,
        },
        RedactPiiSub = SubstitutionPolicy.Hash,
        SpeakerLabels = true,
        SpeakersExpected = 2,
        ContentSafety = true,
        IabCategories = true,
        CustomSpelling = new List<TranscriptCustomSpelling>()
        {
            new TranscriptCustomSpelling
            {
                From = new List<string>() { "dicarlo" },
                To = "Decarlo",
            },
        },
        SentimentAnalysis = true,
        AutoChapters = true,
        EntityDetection = true,
        SpeechThreshold = 0.5f,
        Summarization = true,
        SummaryModel = SummaryModel.Informative,
        SummaryType = SummaryType.Bullets,
        CustomTopics = true,
        Topics = new List<string>() { "topics" },
        AudioUrl = "https://assembly.ai/wildfires.mp3",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `TranscriptParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">GetAsync</a>(transcriptId) -> Transcript</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get the transcript resource. The transcript is ready when the "status" is "completed".
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.GetAsync("transcript_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">DeleteAsync</a>(transcriptId) -> Transcript</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Remove the data from the transcript and mark it as deleted.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.DeleteAsync("{transcript_id}");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">GetSubtitlesAsync</a>(transcriptId, subtitleFormat, GetSubtitlesParams { ... }) -> string</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Export your transcript in SRT or VTT format to use with a video player for subtitles and closed captions.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.GetSubtitlesAsync(
    "string",
    SubtitleFormat.Srt,
    new GetSubtitlesParams { CharsPerCaption = 1 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>

<dl>
<dd>

**subtitleFormat:** `SubtitleFormat` — The format of the captions
    
</dd>
</dl>

<dl>
<dd>

**request:** `GetSubtitlesParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">GetSentencesAsync</a>(transcriptId) -> SentencesResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get the transcript split by sentences. The API will attempt to semantically segment the transcript into sentences to create more reader-friendly transcripts.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.GetSentencesAsync("transcript_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">GetParagraphsAsync</a>(transcriptId) -> ParagraphsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get the transcript split by paragraphs. The API will attempt to semantically segment your transcript into paragraphs to create more reader-friendly transcripts.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.GetParagraphsAsync("transcript_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">WordSearchAsync</a>(transcriptId, WordSearchParams { ... }) -> WordSearchResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Search through the transcript for keywords. You can search for individual words, numbers, or phrases containing up to five words or numbers.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.WordSearchAsync("string", new WordSearchParams { Words = ["string"] });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>

<dl>
<dd>

**request:** `WordSearchParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Transcripts.<a href="/src/AssemblyAI/Transcripts/TranscriptsClient.cs">GetRedactedAudioAsync</a>(transcriptId) -> RedactedAudioResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve the redacted audio object containing the status and URL to the redacted audio.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Transcripts.GetRedactedAudioAsync("transcript_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**transcriptId:** `string` — ID of the transcript
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Realtime
<details><summary><code>client.Realtime.<a href="/src/AssemblyAI/Realtime/RealtimeClient.cs">CreateTemporaryTokenAsync</a>(CreateRealtimeTemporaryTokenParams { ... }) -> RealtimeTemporaryTokenResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a temporary authentication token for Streaming Speech-to-Text
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Realtime.CreateTemporaryTokenAsync(
    new CreateRealtimeTemporaryTokenParams { ExpiresIn = 480 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateRealtimeTemporaryTokenParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## LeMUR
<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">TaskAsync</a>(LemurTaskParams { ... }) -> LemurTaskResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use the LeMUR task endpoint to input your own LLM prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.TaskAsync(
    new LemurTaskParams
    {
        TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
        Context = "This is an interview about wildfires.",
        FinalModel = LemurModel.AnthropicClaude35Sonnet,
        MaxOutputSize = 3000,
        Temperature = 0f,
        Prompt = "List all the locations affected by wildfires.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LemurTaskParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">SummaryAsync</a>(LemurSummaryParams { ... }) -> LemurSummaryResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Custom Summary allows you to distill a piece of audio into a few impactful sentences.
You can give the model context to obtain more targeted results while outputting the results in a variety of formats described in human language.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.SummaryAsync(
    new LemurSummaryParams
    {
        TranscriptIds = new List<string>() { "47b95ba5-8889-44d8-bc80-5de38306e582" },
        Context = "This is an interview about wildfires.",
        FinalModel = LemurModel.AnthropicClaude35Sonnet,
        MaxOutputSize = 3000,
        Temperature = 0f,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LemurSummaryParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">QuestionAnswerAsync</a>(LemurQuestionAnswerParams { ... }) -> LemurQuestionAnswerResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Question & Answer allows you to ask free-form questions about a single transcript or a group of transcripts.
The questions can be any whose answers you find useful, such as judging whether a caller is likely to become a customer or whether all items on a meeting's agenda were covered.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.QuestionAnswerAsync(
    new LemurQuestionAnswerParams
    {
        TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
        Context = "This is an interview about wildfires.",
        FinalModel = LemurModel.AnthropicClaude35Sonnet,
        MaxOutputSize = 3000,
        Temperature = 0f,
        Questions = new List<LemurQuestion>()
        {
            new LemurQuestion
            {
                Question = "Where are there wildfires?",
                AnswerFormat = "List of countries in ISO 3166-1 alpha-2 format",
                AnswerOptions = new List<string>() { "US", "CA" },
            },
            new LemurQuestion
            {
                Question = "Is global warming affecting wildfires?",
                AnswerOptions = new List<string>() { "yes", "no" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LemurQuestionAnswerParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">ActionItemsAsync</a>(LemurActionItemsParams { ... }) -> LemurActionItemsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use LeMUR to generate a list of action items from a transcript
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.ActionItemsAsync(
    new LemurActionItemsParams
    {
        TranscriptIds = new List<string>() { "64nygnr62k-405c-4ae8-8a6b-d90b40ff3cce" },
        Context = "This is an interview about wildfires.",
        FinalModel = LemurModel.AnthropicClaude35Sonnet,
        MaxOutputSize = 3000,
        Temperature = 0f,
        AnswerFormat = "Bullet Points",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LemurActionItemsParams` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">GetResponseAsync</a>(requestId) -> OneOf<LemurStringResponse, LemurQuestionAnswerResponse></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a LeMUR response that was previously generated.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.GetResponseAsync("request_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**requestId:** `string` 

The ID of the LeMUR request you previously made.
This would be found in the response of the original request.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Lemur.<a href="/src/AssemblyAI/Lemur/LemurClient.cs">PurgeRequestDataAsync</a>(requestId) -> PurgeLemurRequestDataResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete the data for a previously submitted LeMUR request.
The LLM response data, as well as any context provided in the original request will be removed.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Lemur.PurgeRequestDataAsync("request_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**requestId:** `string` — The ID of the LeMUR request whose data you want to delete. This would be found in the response of the original request.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>
