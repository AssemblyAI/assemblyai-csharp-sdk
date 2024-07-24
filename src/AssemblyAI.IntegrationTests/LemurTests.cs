namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class LemurTests
{
    private string ApiKey
    {
        get
        {
            var apiKey = TestContext.Parameters.Get("ASSEMBLYAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("ASSEMBLYAI_API_KEY .runsettings parameter is not set.");
            return apiKey;
        }
    }

    private string[] TranscriptIds
    {
        get
        {
            var transcriptIds = TestContext.Parameters.Get("TEST_TRANSCRIPT_IDS");
            if (string.IsNullOrEmpty(transcriptIds))
                throw new Exception("TEST_TRANSCRIPT_IDS .runsettings parameter is not set.");
            return transcriptIds.Split(',');
        }
    }

    // TODO: uncomment when Fern fixes params generation
    /*[Test]
    public async Task Should_Generate_Summary()
    {
        var client = new AssemblyAIClient(ApiKey);
        var response = await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.RequestId, Is.Not.Empty);
        Assert.That(response.Response, Is.Not.Empty);
    }

    [Test]
    public async Task Should_Generate_Answer()
    {
        var client = new AssemblyAIClient(ApiKey);
        var response = await client.Lemur.QuestionAnswerAsync(new LemurQuestionAnswerParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            Questions = new[]
            {
                new LemurQuestion
                {
                    Question = "What are they discussing?",
                    AnswerFormat = "text"
                }
            }
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.RequestId, Is.Not.Empty);
        Assert.That(response.Response, Is.Not.Empty);
        Assert.That(response.Response.First().Question, Is.Not.Empty);
        Assert.That(response.Response.First().Answer, Is.Not.Empty);
    }

    [Test]
    public async Task Should_Generate_Action_Items()
    {
        var client = new AssemblyAIClient(ApiKey);
        var response = await client.Lemur.ActionItemsAsync(new LemurActionItemsParams()
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.RequestId, Is.Not.Empty);
        Assert.That(response.Response, Is.Not.Empty);
    }

    [Test]
    public async Task Should_Generate_Task()
    {
        var client = new AssemblyAIClient(ApiKey);
        var response = await client.Lemur.TaskAsync(new LemurTaskParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            Prompt = "Write a haiku about this conversation."
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.RequestId, Is.Not.Empty);
        Assert.That(response.Response, Is.Not.Empty);
    }

    [Test]
    public void Should_Fail_To_Generate_Summary()
    {
        var client = new AssemblyAIClient(ApiKey);
        var ex = Assert.ThrowsAsync<Exception>(async () => await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = ["bad-id"],
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false));

        Assert.That(ex.Message, Is.EqualTo("each transcript source id must be valid"));
    }

    [Test]
    public async Task Should_Return_Response()
    {
        var client = new AssemblyAIClient(ApiKey);
        var taskResponse = await client.Lemur.TaskAsync(new LemurTaskParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            Prompt = "Write a haiku about this conversation."
        }).ConfigureAwait(false);

        var taskResponse2OneOf = await client.Lemur.GetResponseAsync(taskResponse.RequestId).ConfigureAwait(false);
        var taskResponse2 = (LemurStringResponse) taskResponse2OneOf.Value;
        Assert.That(taskResponse2.RequestId, Is.EqualTo(taskResponse.RequestId));
        Assert.That(taskResponse2.Response, Is.EqualTo(taskResponse.Response));
    }

    [Test]
    public async Task Should_Purge_Request_Data()
    {
        var client = new AssemblyAIClient(ApiKey);
        var summaryResponse = await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false);

        var deletionRequest = await client.Lemur.PurgeRequestDataAsync(summaryResponse.RequestId).ConfigureAwait(false);
        Assert.That(deletionRequest.Deleted, Is.True);
        Assert.That(summaryResponse.RequestId, Is.EqualTo(deletionRequest.RequestIdToPurge));
    }*/
}