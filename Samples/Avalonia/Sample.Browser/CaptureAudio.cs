using System;
using System.Runtime.InteropServices.JavaScript;
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

[SupportedOSPlatform("browser")]
public static partial class JsMicrophone
{
    public static event OnAudioData? OnAudioData;

    [JSImport("hasPermission", "audio")]
    internal static partial Task<bool> HasPermission();

    [JSImport("requestPermission", "audio")]
    internal static partial Task RequestPermissionJs();


    [JSImport("startRecording", "audio")]
    internal static partial Task StartRecording();


    [JSImport("stopRecording", "audio")]
    internal static partial void StopRecording();

    [JSExport]
    public static async Task LoadModule() => await JSHost.ImportAsync("audio", "/audio.mjs");

    [JSExport]
    public static void OnAudioDataFromJs(byte[] audio) => OnAudioData?.Invoke(audio);
}
