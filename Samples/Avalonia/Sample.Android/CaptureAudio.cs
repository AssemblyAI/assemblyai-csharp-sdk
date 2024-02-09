using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Media;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace Sample.Android;

public class CaptureAudio : ICaptureAudio
{
    private const string RecordPermissionName = "android.permission.RECORD_AUDIO";
    private readonly Activity _activity;
    private CancellationTokenSource _cts;
    public event OnAudioData? OnAudioData;

    public CaptureAudio(Activity activity)
    {
        _activity = activity;
    }


    public Task<bool> HasPermission() => 
        Task.FromResult(ContextCompat.CheckSelfPermission(_activity, RecordPermissionName) == Permission.Granted);

    public void RequestPermission()
    {
        ActivityCompat.RequestPermissions(_activity, [RecordPermissionName], 0);
    }

    public void Start()
    {
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        var thread = new Thread(() =>
        {
            const int sampleRate = 16_000;
            const int bufferSize = 1024;

            var audioRecord =
                new AudioRecord(
                    AudioSource.Mic,
                    sampleRate,
                    ChannelIn.Mono,
                    Encoding.Pcm16bit,
                    bufferSize
                );
            audioRecord.StartRecording();

            var buffer = new byte[bufferSize];
            while (!ct.IsCancellationRequested)
            {
                audioRecord.Read(buffer, 0, bufferSize);
                OnAudioData?.Invoke(buffer);
            }
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public void Stop()
    {
        _cts.Cancel();
    }
}