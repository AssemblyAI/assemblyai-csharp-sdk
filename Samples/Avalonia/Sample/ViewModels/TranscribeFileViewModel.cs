using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AssemblyAI;
using AssemblyAI.Lemur;
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

    private Transcript _transcript = new()
    {
        Text = "Your transcript will appear here.",
        Id = null,
        LanguageModel = null,
        AcousticModel = null,
        Status = TranscriptStatus.Queued,
        AudioUrl = null,
        WebhookAuth = false,
        AutoHighlights = false,
        RedactPii = false,
        Summarization = false
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
        try
        {
            IsTranscribing = true;
            await using var fileStream = await _selectedFile.OpenReadAsync();
            var transcript = await _client.Transcripts.TranscribeAsync(fileStream, new TranscriptOptionalParams
            {
                LanguageCode = SelectedLanguage != null
                    ? EnumConverter.ToEnum<TranscriptLanguageCode>(SelectedLanguage.Value.Key)
                    : null
            });
            Transcript = transcript;
        }
        catch (Exception e)
        {
            Transcript.Text = $"Error: {e.Message}";
            Console.WriteLine(e);
        }
        IsTranscribing = false;
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
        var prompt = Prompt;
        Prompt = "";
        LemurMessages.Add(LemurMessageViewModel.FromUser(prompt));
        IsLemurThinking = true;
        var response = await _client.Lemur.TaskAsync(new LemurTaskParams
        {
            TranscriptIds = new[] {Transcript.Id},
            Prompt = prompt
        });
        LemurMessages.Add(LemurMessageViewModel.FromAssistant(response.Response.Trim()));
        IsLemurThinking = false;
    }
}