namespace AssemblyAI.IntegrationTests;

internal static class AssemblyAITestParameters
{
    internal static string AssemblyAIEndpoint =>
        TestContext.Parameters.Get("ASSEMBLYAI_ENDPOINT")
        ?? Environment.GetEnvironmentVariable("ASSEMBLYAI_ENDPOINT")
        ?? AssemblyAIClientEnvironment.Default;

    internal static string ApiKey
    {
        get
        {
            var apiKey = TestContext.Parameters.Get("ASSEMBLYAI_API_KEY")
                ?? Environment.GetEnvironmentVariable("ASSEMBLYAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("ASSEMBLYAI_API_KEY parameter is not set.");
            return apiKey;
        }
    }

    internal static string TranscriptId
    {
        get
        {
            var transcriptId = TestContext.Parameters.Get("TEST_TRANSCRIPT_ID")
                               ?? Environment.GetEnvironmentVariable("TEST_TRANSCRIPT_ID");
            if (string.IsNullOrEmpty(transcriptId))
                throw new Exception("TEST_TRANSCRIPT_ID parameter is not set.");
            return transcriptId;
        }
    }

    internal static string[] TranscriptIds
    {
        get
        {
            var transcriptIds = TestContext.Parameters.Get("TEST_TRANSCRIPT_IDS")
                                ?? Environment.GetEnvironmentVariable("TEST_TRANSCRIPT_IDS");
            if (string.IsNullOrEmpty(transcriptIds))
                throw new Exception("TEST_TRANSCRIPT_IDS parameter is not set.");
            return transcriptIds.Split(',');
        }
    }
}