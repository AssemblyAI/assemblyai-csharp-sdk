using System;

namespace Sample;

public delegate void OnAudioData(byte[] audio);

public interface ICaptureAudio
{
    public event OnAudioData OnAudioData;
    public bool HasPermission();
    public void RequestPermission();
    public void Start();
    public void Stop();
}