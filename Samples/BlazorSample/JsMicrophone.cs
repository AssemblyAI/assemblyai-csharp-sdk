using System.Runtime.Versioning;
using Microsoft.JSInterop;

namespace BlazorSample;

[SupportedOSPlatform("browser")]
public class JsMicrophone : IAsyncDisposable
{
    private IJSObjectReference _jsObjectReference;
    public event Action<byte[]>? OnAudioData;

    public static async Task<JsMicrophone> CreateAsync(IJSRuntime jsRuntime, string modulePath)
    {
        var jsModuleObject = await jsRuntime.InvokeAsync<IJSObjectReference>("import", modulePath);
        var jsMicrophone = new JsMicrophone();
        var dotNetObjectReference = DotNetObjectReference.Create(jsMicrophone);
        var jsMicrophoneObject = await jsModuleObject.InvokeAsync<IJSObjectReference>("create", dotNetObjectReference);
        jsMicrophone._jsObjectReference = jsMicrophoneObject;
        return jsMicrophone;
    }

    public ValueTask StartRecordingAsync() => _jsObjectReference.InvokeVoidAsync("startRecording");

    public void StopRecordingAsync() =>  _jsObjectReference.InvokeVoidAsync("stopRecording");

    [JSInvokable]
    public void OnAudioDataFromJs(byte[] audio) => OnAudioData?.Invoke(audio);

    public async ValueTask DisposeAsync()
    {
        OnAudioData = null;
        await _jsObjectReference.DisposeAsync();
    }
}