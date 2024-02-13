using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace Sample.Browser;

[SupportedOSPlatform("browser")]
public class CaptureAudio : ICaptureAudio, IDisposable
{

    public event OnAudioData? OnAudioData;

    public CaptureAudio()
    {
        JsMicrophone.OnAudioData += JsMicrophoneOnOnAudioData;
    }

    private void JsMicrophoneOnOnAudioData(byte[] audio) => OnAudioData?.Invoke(audio);

    public Task<bool> HasPermission() => JsMicrophone.HasPermission();

    public void RequestPermission() => JsMicrophone.RequestPermissionJs();

    public async void Start() => await JsMicrophone.StartRecording();

    public async void Stop() => JsMicrophone.StopRecording();

    public void Dispose()
    {
        JsMicrophone.OnAudioData -= JsMicrophoneOnOnAudioData;
    }
}