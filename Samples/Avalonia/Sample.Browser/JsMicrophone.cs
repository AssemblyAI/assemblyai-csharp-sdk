using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace Sample.Browser;

[SupportedOSPlatform("browser")]
public static partial class JsMicrophone
{
    public static OnAudioData? OnAudioData { get; set; }

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
    public static Task OnAudioDataFromJs(byte[] audio) => OnAudioData?.Invoke(audio) ?? Task.CompletedTask;
}