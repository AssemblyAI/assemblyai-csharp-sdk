namespace AssemblyAI.IntegrationTests;

internal static class AssemblyAITestParameters
{
    
    internal static string ApiKey
    {
        get
        {
            var apiKey = TestContext.Parameters.Get("ASSEMBLYAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("ASSEMBLYAI_API_KEY .runsettings parameter is not set.");
            return apiKey;
        }
    }

    internal static string TranscriptId
    {
        get
        {
            var transcriptId = TestContext.Parameters.Get("TEST_TRANSCRIPT_ID");
            if (string.IsNullOrEmpty(transcriptId))
                throw new Exception("TEST_TRANSCRIPT_ID .runsettings parameter is not set.");
            return transcriptId;
        }
    }

    internal static string[] TranscriptIds
    {
        get
        {
            var transcriptIds = TestContext.Parameters.Get("TEST_TRANSCRIPT_IDS");
            if (string.IsNullOrEmpty(transcriptIds))
                throw new Exception("TEST_TRANSCRIPT_IDS .runsettings parameter is not set.");
            return transcriptIds.Split(',');
        }
    }
}