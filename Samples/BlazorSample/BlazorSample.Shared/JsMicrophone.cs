using System.Runtime.Versioning;
using Microsoft.JSInterop;

namespace BlazorSample.Shared;

public delegate Task OnAudioData(byte[] audio);

[SupportedOSPlatform("browser")]
public class JsMicrophone : IAsyncDisposable
{
    private IJSObjectReference _jsObjectReference;
    public OnAudioData? OnAudioData { get; set; }

    public static async Task<JsMicrophone> CreateAsync(IJSRuntime jsRuntime)
    {
        var jsModuleObject = await jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", 
            "./_content/BlazorSample.Shared/audio.mjs"
        );
        var jsMicrophone = new JsMicrophone();
        var dotNetObjectReference = DotNetObjectReference.Create(jsMicrophone);
        var jsMicrophoneObject = await jsModuleObject.InvokeAsync<IJSObjectReference>("create", dotNetObjectReference);
        jsMicrophone._jsObjectReference = jsMicrophoneObject;
        return jsMicrophone;
    }

    public ValueTask StartRecordingAsync() => _jsObjectReference.InvokeVoidAsync("startRecording");

    public void StopRecordingAsync() =>  _jsObjectReference.InvokeVoidAsync("stopRecording");

    [JSInvokable]
    public Task OnAudioDataFromJs(byte[] audio) => OnAudioData?.Invoke(audio) ?? Task.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        OnAudioData = null;
        await _jsObjectReference.DisposeAsync();
    }
}