namespace AssemblyAI.IntegrationTests;

public static class Helpers
{
    public static AssemblyAIClient CreateClient()
    {
        var endpoint = AssemblyAITestParameters.AssemblyAIEndpoint;
        var apiKey = AssemblyAITestParameters.ApiKey;
        return new AssemblyAIClient(new ClientOptions
        {
            BaseUrl = endpoint,
            ApiKey = apiKey
        });
    }
}