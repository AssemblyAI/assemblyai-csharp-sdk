using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;

namespace Sample.Desktop;

public class CaptureAudio : ICaptureAudio
{
    private CancellationTokenSource? _cts;
    public event OnAudioData? OnAudioData;

    public Task<bool> HasPermission() => Task.FromResult(true);

    public void RequestPermission()
    {
    }

    public void Start()
    {
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        var thread = new Thread(() =>
        {
            const int sampleRate = 16_000;
            const int bufferSize = 1024;
            var captureDevice = ALC.CaptureOpenDevice(null, sampleRate, ALFormat.Mono16, bufferSize);
            ALC.CaptureStart(captureDevice);

            var buffer = new byte[bufferSize];
            while (true)
            {
                try
                {
                    var current = 0;
                    while (current < buffer.Length && !ct.IsCancellationRequested)
                    {
                        var samplesAvailable = ALC.GetInteger(captureDevice, AlcGetInteger.CaptureSamples);
                        if (samplesAvailable < 512) continue;
                        var samplesToRead = Math.Min(samplesAvailable, buffer.Length - current);
                        ALC.CaptureSamples(captureDevice, ref buffer[current], samplesToRead);
                        current += samplesToRead;
                    }

                    if (ct.IsCancellationRequested) break;

                    // copy  buffer so that it can be used in another thread
                    OnAudioData?.Invoke(buffer.ToArray());
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public void Stop()
    {
        _cts?.Cancel();
    }
}