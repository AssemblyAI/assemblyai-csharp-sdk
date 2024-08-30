using AssemblyAI.Realtime;
using Moq;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class RealtimeTranscriberTests
{
    [Test]
    public async Task Should_Transcribe_With_Api_Key()
    {
        var client = new AssemblyAIClient(AssemblyAITestParameters.ApiKey);
        // Set up the transcriber
        // ReSharper disable once UseAwaitUsing
        using var transcriber = client.Realtime.CreateTranscriber(new RealtimeTranscriberOptions
        {
            Encoding = AudioEncoding.PcmS16le,
            WordBoost = ["foo", "bar"],
            SampleRate = 16_000,
            DisablePartialTranscripts = false
        });
        await TestTranscriber(transcriber).ConfigureAwait(false);
    }

    [Test]
    public async Task Should_Transcribe_With_Token()
    {
        var client = new AssemblyAIClient(AssemblyAITestParameters.ApiKey);
        var tokenResponse = await client.Realtime.CreateTemporaryTokenAsync(480);

        // Set up the transcriber
        // ReSharper disable once UseAwaitUsing
        using var transcriber = new RealtimeTranscriber(new RealtimeTranscriberOptions
        {
            Token = tokenResponse.Token,
            Encoding = AudioEncoding.PcmS16le,
            WordBoost = ["foo", "bar"],
            SampleRate = 16_000,
            DisablePartialTranscripts = false
        });
        await TestTranscriber(transcriber).ConfigureAwait(false);
    }

    private static async Task TestTranscriber(RealtimeTranscriber transcriber)
    {
        // Arrange
        var listener = Mock.Of<IRealtimeTranscriberListener>();
        transcriber.SessionBegins.Subscribe(listener.OnSessionBegins);
        transcriber.TranscriptReceived.Subscribe(listener.OnTranscriptReceived);
        transcriber.PartialTranscriptReceived.Subscribe(listener.OnPartialTranscriptReceived);
        transcriber.FinalTranscriptReceived.Subscribe(listener.OnFinalTranscriptReceived);
        transcriber.SessionInformationReceived.Subscribe(listener.OnSessionInformationReceived);
        transcriber.Closed.Subscribe(listener.OnClosed);
        transcriber.ErrorReceived.Subscribe(listener.OnErrorReceived);
        transcriber.ExceptionOccurred.Subscribe(listener.OnExceptionOccurred);

        await transcriber.ConnectAsync().ConfigureAwait(false);

        // Act

        _ = Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(
            _ => transcriber.ForceEndUtteranceAsync().ConfigureAwait(false)
        );

        try
        {
            var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "gore-short.wav");
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var ct = cts.Token;
            using var fileStream = File.OpenRead(testFilePath);
            var audioChunk = new byte[8192];
            while (
                ct.IsCancellationRequested == false &&
                // ReSharper disable once MethodHasAsyncOverloadWithCancellation
                fileStream.Read(audioChunk, 0, audioChunk.Length) > 0
            )
            {
                await transcriber.SendAudioAsync(audioChunk).ConfigureAwait(false);
                await Task.Delay(100, ct).ConfigureAwait(false);
            }
        }
        catch (TaskCanceledException)
        {
            // ignore
        }

        await transcriber.CloseAsync().ConfigureAwait(false);

        // Assert
        var listenerMock = Mock.Get(listener);
        listenerMock.Verify(l => l.OnSessionBegins(
                It.Is<SessionBegins>(s =>
                    s.MessageType == "SessionBegins" &&
                    s.ExpiresAt > DateTime.UtcNow &&
                    string.IsNullOrEmpty(s.SessionId) == false
                )),
            Times.Once
        );
        listenerMock.Verify(l => l.OnTranscriptReceived(
                It.Is<RealtimeTranscript>(t =>
                    (t.IsPartialTranscript && VerifyPartialTranscript(t.AsPartialTranscript)) ||
                    (t.IsFinalTranscript && VerifyFinalTranscript(t.AsFinalTranscript)))
            ),
            Times.AtLeastOnce
        );
        listenerMock.Verify(l => l.OnPartialTranscriptReceived(
                It.Is<PartialTranscript>(t => VerifyPartialTranscript(t))),
            Times.AtLeastOnce
        );
        listenerMock.Verify(l => l.OnFinalTranscriptReceived(
                It.Is<FinalTranscript>(t => VerifyFinalTranscript(t))),
            Times.AtLeastOnce
        );
        listenerMock.Verify(l => l.OnSessionInformationReceived(
                It.Is<SessionInformation>(i =>
                    i.MessageType == "SessionInformation" &&
                    i.AudioDurationSeconds > 0
                )),
            Times.Once
        );
        listenerMock.Verify(l => l.OnErrorReceived(It.IsAny<RealtimeError>()), Times.Never);
        listenerMock.Verify(l => l.OnClosed(
                It.Is<ClosedEventArgs>(
                    e => e.Code == 1000 && string.IsNullOrEmpty(e.Reason) == false
                )),
            Times.Once
        );
        listenerMock.Verify(l => l.OnExceptionOccurred(It.IsAny<Exception>()), Times.Never);
    }

    private static bool VerifyPartialTranscript(PartialTranscript t) =>
        t.MessageType == "PartialTranscript";

    private static bool VerifyFinalTranscript(FinalTranscript t) =>
        t.MessageType == "FinalTranscript" &&
        t.Text.Length > 0 &&
        t.Words.Any();
}

public interface IRealtimeTranscriberListener
{
    void OnSessionInformationReceived(SessionInformation sessionInformation);
    void OnSessionBegins(SessionBegins sessionBegins);
    void OnTranscriptReceived(RealtimeTranscript transcript);
    void OnPartialTranscriptReceived(PartialTranscript transcript);
    void OnFinalTranscriptReceived(FinalTranscript transcript);
    void OnClosed(ClosedEventArgs closedEvent);
    void OnExceptionOccurred(Exception exception);
    void OnErrorReceived(RealtimeError error);
}