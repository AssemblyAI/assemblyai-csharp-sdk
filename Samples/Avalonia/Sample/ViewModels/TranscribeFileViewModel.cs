using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AssemblyAI;
using AssemblyAI.Transcripts;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Sample.ViewModels;

public class TranscribeFileViewModel : ViewModelBase
{
    private static readonly Dictionary<string, string> _supportedLanguages = new()
    {
        ["ALD"] = "Automatic Language Detection",
        ["en_us"] = "American English",
        ["en_au"] = "Australian English",
        ["en_uk"] = "British English",
        ["zh"] = "Chinese",
        ["nl"] = "Dutch",
        ["fi"] = "Finnish",
        ["fr"] = "French",
        ["en"] = "Global English",
        ["de"] = "German",
        ["hi"] = "Hindi",
        ["it"] = "Italian",
        ["ja"] = "Japanese",
        ["ko"] = "Korean",
        ["pl"] = "Polish",
        ["pt"] = "Portuguese",
        ["ru"] = "Russian",
        ["es"] = "Spanish",
        ["tr"] = "Turkish",
        ["uk"] = "Ukrainian",
        ["vi"] = "Vietnamese"
    };

    private IStorageFile? _selectedFile;

    public IStorageFile? SelectedFile
    {
        get => _selectedFile;
        set => this.RaiseAndSetIfChanged(ref _selectedFile, value);
    }

    public IEnumerable<KeyValuePair<string, string>> SupportedLanguages => _supportedLanguages.AsEnumerable();

    private KeyValuePair<string, string>? _selectedLanguage = _supportedLanguages.AsEnumerable().First();

    public KeyValuePair<string, string>? SelectedLanguage
    {
        get => _selectedLanguage;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
    }

    private bool _isTranscribing;

    public bool IsTranscribing
    {
        get => _isTranscribing;
        set => this.RaiseAndSetIfChanged(ref _isTranscribing, value);
    }

    private Transcript _transcript = new Transcript
    {
        Text =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n"
    };

    public Transcript Transcript
    {
        get => _transcript;
        set => this.RaiseAndSetIfChanged(ref _transcript, value);
    }

    public async Task OnUploadFileClick(Visual visual)
    {
        var topLevel = TopLevel.GetTopLevel(visual);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Pick Audio File",
            AllowMultiple = false
        });

        if (files.Count < 1 || files.Count > 1) throw new UnreachableException();

        SelectedFile = files[0];
    }

    public async Task Transcribe()
    {
        IsTranscribing = true;
        await using var fileStream = await _selectedFile.OpenReadAsync();
        var uploadedFile = await _client.Files.UploadAsync(fileStream);
        var transcript = await _client.Transcripts.SubmitAsync(new TranscriptParams
        {
            AudioUrl = uploadedFile.UploadUrl,
            LanguageCode = SelectedLanguage != null
                ? Enum.Parse<TranscriptLanguageCode>(SelectedLanguage.Value.Value)
                : null
        });
        while (true)
        {
            if (transcript.Status == TranscriptStatus.Error) throw new Exception();
            if (transcript.Status == TranscriptStatus.Completed) break;
            await Task.Delay(500);
            transcript = await _client.Transcripts.GetAsync(transcript.Id);
        }

        Transcript = transcript;
        IsTranscribing = false;
    }

    // TODO: Replace when stream is supported by SDK
    public static byte[] ReadToEnd(System.IO.Stream stream)
    {
        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }

            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }

    private string _prompt = "";

    public string Prompt
    {
        get => _prompt;
        set => this.RaiseAndSetIfChanged(ref _prompt, value);
    }

    private bool _isLemurThinking = false;
    private readonly AssemblyAIClient _client = DiContainer.Services.GetRequiredService<AssemblyAIClient>();

    public bool IsLemurThinking
    {
        get => _isLemurThinking;
        set => this.RaiseAndSetIfChanged(ref _isLemurThinking, value);
    }

    public ObservableCollection<LemurMessageViewModel> LemurMessages { get; } =
        [LemurMessageViewModel.FromAssistant("Ask a question about your transcript.")];

    public async Task AskQuestion()
    {
        LemurMessages.Add(LemurMessageViewModel.FromUser(Prompt));
        Prompt = "";
        IsLemurThinking = true;
        await Task.Delay(1_000);
        LemurMessages.Add(LemurMessageViewModel.FromAssistant("Response"));
        IsLemurThinking = false;
    }
}