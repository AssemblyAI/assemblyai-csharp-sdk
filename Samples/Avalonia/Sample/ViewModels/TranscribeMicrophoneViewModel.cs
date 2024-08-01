using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AssemblyAI;
using AssemblyAI.Realtime;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Sample.ViewModels;

public class TranscribeMicrophoneViewModel : ViewModelBase, IAsyncDisposable
{
    private readonly ICaptureAudio _captureAudio;
    private readonly AssemblyAIClient _client = DiContainer.Services.GetRequiredService<AssemblyAIClient>();
    private readonly SortedDictionary<int, string> _transcriptWords = new();
    private readonly RealtimeTranscriber _transcriber;

    private string _transcript = "Your transcript will appear here.";
    public string Transcript
    {
        get => _transcript;
        set => this.RaiseAndSetIfChanged(ref _transcript, value);
    }

    private string  _error;

    public string  Error
    {
        get => _error;
        set => this.RaiseAndSetIfChanged(ref _error, value);
    }

    private bool _isConnecting;

    public bool IsConnecting
    {
        get => _isConnecting;
        set => this.RaiseAndSetIfChanged(ref _isConnecting, value);
    }

    private bool _isConnected;

    public bool IsConnected
    {
        get => _isConnected;
        set => this.RaiseAndSetIfChanged(ref _isConnected, value);
    }

    private bool _isDisconnecting;

    public bool IsDisconnecting
    {
        get => _isDisconnecting;
        set => this.RaiseAndSetIfChanged(ref _isDisconnecting, value);
    }


    private bool _isDisconnected = true;
    private CancellationTokenSource _transcribeCancellationTokenSource;

    public bool IsDisconnected
    {
        get => _isDisconnected;
        set => this.RaiseAndSetIfChanged(ref _isDisconnected, value);
    }

    public TranscribeMicrophoneViewModel()
    {
        _captureAudio = DiContainer.Services.GetRequiredService<ICaptureAudio>();
        _transcriber = new RealtimeTranscriber();
        _transcriber.PartialTranscriptReceived.Subscribe(OnPartialTranscriptReceived);
        _transcriber.FinalTranscriptReceived.Subscribe(OnFinalTranscriptReceived);
        _transcriber.ErrorReceived.Subscribe(OnErrorReceived);
        _transcriber.Closed.Subscribe(OnClosed);
        _transcriber.ObservableForProperty(t => t.Status)
            .Subscribe(status =>
            {
                switch (status.Value)
                {
                    case RealtimeTranscriberStatus.Connected:
                        IsConnecting = false;
                        IsConnected = true;
                        IsDisconnecting = false;
                        IsDisconnected = false;
                        break;
                    case RealtimeTranscriberStatus.Connecting:
                        IsConnecting = true;
                        IsConnected = false;
                        IsDisconnecting = false;
                        IsDisconnected = false;
                        break;
                    case RealtimeTranscriberStatus.Disconnecting:
                        IsConnecting = false;
                        IsConnected = false;
                        IsDisconnecting = true;
                        IsDisconnected = false;
                        break;
                    case RealtimeTranscriberStatus.Disconnected:
                        IsConnecting = false;
                        IsConnected = false;
                        IsDisconnecting = false;
                        IsDisconnected = true;
                        break;
                    default:
                        throw new UnreachableException();
                }
            });
        _captureAudio.OnAudioData += CaptureAudioOnOnAudioData;
    }

    private void CaptureAudioOnOnAudioData(byte[] audio)
    {
        _transcriber.SendAudio(audio);
    }

    private void OnErrorReceived(RealtimeError error)
    {
        Error = error.Error;
    }

    private void OnClosed(ClosedEventArgs closedEvent)
    {
        if (closedEvent.Code == (int)WebSocketCloseStatus.NormalClosure) return;
        Error = $"Socket closed with code {closedEvent.Code}: {closedEvent.Reason}";
    }

    private void OnPartialTranscriptReceived(PartialTranscript partialTranscript)
    {
        if (partialTranscript.Text == "") return;
        foreach (var word in partialTranscript.Words)
        {
            _transcriptWords[word.Start] = word.Text;
        }

        BuildTranscript();
    }

    private void OnFinalTranscriptReceived(FinalTranscript finalTranscript)
    {
        foreach (var word in finalTranscript.Words)
        {
            _transcriptWords[word.Start] = word.Text;
        }

        BuildTranscript();
    }

    private void BuildTranscript()
    {
        var stringBuilder = new StringBuilder();
        foreach (var word in _transcriptWords.Values)
        {
            stringBuilder.Append($"{word} ");
        }

        Transcript = stringBuilder.ToString();
    }

    public async Task StartAsync()
    {
        if (!await _captureAudio.HasPermission())
        {
            _captureAudio.RequestPermission();
            return;
        }

        try
        {
            _transcriber.Token = (await _client.Realtime.CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams
            {
                ExpiresIn = 360
            })).Token;

            await _transcriber.ConnectAsync();
            _captureAudio.Start();
            Error = null;
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    public async Task StopAsync()
    {
        try
        {
            _captureAudio.Stop();
            await _transcriber.CloseAsync();
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    public async ValueTask DisposeAsync()
    {
        _captureAudio.OnAudioData -= CaptureAudioOnOnAudioData;
        await _transcriber.DisposeAsync();
    }
}