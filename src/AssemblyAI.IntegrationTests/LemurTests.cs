using AssemblyAI.Core;
using AssemblyAI.Lemur;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class LemurTests
{
    private static string ApiKey => AssemblyAITestParameters.ApiKey;
    private static string[] TranscriptIds => AssemblyAITestParameters.TranscriptIds;

    [Test]
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
    [Ignore("Ignore until fixed")]
    public void Should_Fail_To_Generate_Summary()
    {
        var client = new AssemblyAIClient(ApiKey);
        var ex = Assert.ThrowsAsync<ApiException>(async () => await client.Lemur.SummaryAsync(new LemurSummaryParams
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
        var taskResponse2 = taskResponse2OneOf.AsT0;
        Assert.That(taskResponse2.RequestId, Is.EqualTo(taskResponse.RequestId));
        Assert.That(taskResponse2.Response, Is.EqualTo(taskResponse.Response));


        var qaResponse = await client.Lemur.QuestionAnswerAsync(new LemurQuestionAnswerParams
        {
            FinalModel = LemurModel.Basic,
            TranscriptIds = TranscriptIds,
            Questions =
            [
                new LemurQuestion
                {
                    Question = "What are they discussing?",
                    AnswerFormat = "text"
                }
            ]
        }).ConfigureAwait(false);

        var qaResponse2OneOf = await client.Lemur.GetResponseAsync(qaResponse.RequestId).ConfigureAwait(false);
        var qaResponse2 = qaResponse2OneOf.AsT1;
        Assert.That(qaResponse2.RequestId, Is.EqualTo(qaResponse.RequestId));
        Assert.That(qaResponse2.Response.Count(), Is.EqualTo(qaResponse.Response.Count()));
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
    }
}