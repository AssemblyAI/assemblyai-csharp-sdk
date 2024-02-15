using System;
using System.Threading;
using System.Threading.Tasks;
using AssemblyAI.Realtime;
using TMPro;
using UnityEngine;

public class RealtimeTranscriberScript : MonoBehaviour
{
    private TextMeshProUGUI _transcriptText;
    private RealtimeTranscriber _realtimeTranscriber;
    private AudioClip _clip;
    private SynchronizationContext _mainThreadContext;

    public RealtimeTranscriberScript()
    {
        _realtimeTranscriber = new RealtimeTranscriber();
        _realtimeTranscriber.ApiKey = "770aa245eeb74691ac4f132220c65329";
    }
    
    // Start is called before the first frame update
    async void Start()
    {
        _mainThreadContext = SynchronizationContext.Current;
        _transcriptText = GetComponent<TextMeshProUGUI>();
        _clip = Microphone.Start(null, true, 1, 16_000);
        await StartTranscribingAsync().ConfigureAwait(true);
    }

    private async Task StartTranscribingAsync()
    {
        _realtimeTranscriber.SessionBegins += RealtimeTranscriberOnSessionBegins;
        _realtimeTranscriber.Closed += RealtimeTranscriberOnClosed;
        _realtimeTranscriber.ErrorReceived += RealtimeTranscriberOnErrorReceived;
        _realtimeTranscriber.PartialTranscriptReceived += RealtimeTranscriberOnPartialTranscriptReceived;
        _realtimeTranscriber.FinalTranscriptReceived += RealtimeTranscriberOnFinalTranscriptReceived;
        await _realtimeTranscriber.ConnectAsync().ConfigureAwait(false);
    }

    private void RealtimeTranscriberOnSessionBegins(RealtimeTranscriber sender, SessionBeginsEventArgs evt)
    {
        Debug.Log($"Session begins: {evt.Result.SessionId} - {evt.Result.ExpiresAt}");
    }

    private void RealtimeTranscriberOnClosed(RealtimeTranscriber sender, ClosedEventArgs evt)
    {
        Debug.LogError($"Closed: {evt.Code} - {evt.Reason}");
    }

    private void RealtimeTranscriberOnErrorReceived(RealtimeTranscriber sender, ErrorEventArgs evt)
    {
        Debug.LogError(evt.Error);
    }

    private void RealtimeTranscriberOnPartialTranscriptReceived(RealtimeTranscriber sender,
        PartialTranscriptEventArgs evt)
    {
        if (evt.Result.Text == "") return;
        Debug.Log($"Partial transcript: {evt.Result.Text}");
        _mainThreadContext.Post(_ => _transcriptText.text = evt.Result.Text, null);
    }

    private void RealtimeTranscriberOnFinalTranscriptReceived(RealtimeTranscriber sender, FinalTranscriptEventArgs evt)
    {
        Debug.Log($"Final transcript: {evt.Result.Text}");
        _mainThreadContext.Post(_ => _transcriptText.text = evt.Result.Text, null);
    }

    // Update is called once per frame
    void Update()
    {
        var position = Microphone.GetPosition(null);
        if (position != 0)
        {
            return;
        }

        var audioFragment = new float[_clip.samples];
        _clip.GetData(audioFragment, 0);

        var audioFragmentAsBytes = new byte[audioFragment.Length * 4];
        Buffer.BlockCopy(audioFragment, 0, audioFragmentAsBytes, 0, audioFragmentAsBytes.Length);
        if (_realtimeTranscriber.Status == RealtimeTranscriberStatus.Connected)
        {
            Debug.Log(Convert.ToBase64String(audioFragmentAsBytes));
            _realtimeTranscriber.SendAudio(audioFragmentAsBytes);
        }
    }
}