namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class RealtimeClientTests
{
    private static string ApiKey => AssemblyAITestParameters.ApiKey;

    [Test]
    public async Task Should_Generate_TemporaryToken()
    {
        var client = new AssemblyAIClient(ApiKey);
        var tokenResponse = await client.Realtime.CreateTemporaryTokenAsync(new CreateRealtimeTemporaryTokenParams
            {
                ExpiresIn = 480
            })
            .ConfigureAwait(false);

        Assert.That(tokenResponse, Is.Not.Null);
        Assert.That(tokenResponse.Token, Is.Not.Empty);
    }
}