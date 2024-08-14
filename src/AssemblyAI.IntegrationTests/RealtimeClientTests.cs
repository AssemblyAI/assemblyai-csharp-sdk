using AssemblyAI.Realtime;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class RealtimeClientTests
{
    [Test]
    public async Task Should_Generate_TemporaryToken()
    {
        var client = Helpers.CreateClient();
        var tokenResponse = await client.Realtime.CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams
            {
                ExpiresIn = 480
            })
            .ConfigureAwait(false);

        Assert.That(tokenResponse, Is.Not.Null);
        Assert.That(tokenResponse.Token, Is.Not.Empty);
    }
}