using System;
using System.Threading.Tasks;

namespace Sample;

public delegate Task OnAudioData(byte[] audio);

public interface ICaptureAudio
{
    public event OnAudioData OnAudioData;
    public Task<bool> HasPermission();
    public void RequestPermission();
    public void Start();
    public void Stop();
}