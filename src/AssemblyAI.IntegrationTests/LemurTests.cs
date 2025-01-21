using AssemblyAI.Lemur;

namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class LemurTests
{
    private static string[] TranscriptIds => AssemblyAITestParameters.TranscriptIds;

    [Test]
    public async Task Should_Generate_Summary()
    {
        var client = Helpers.CreateClient();
        var response = await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = TranscriptIds,
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response.RequestId, Is.Not.Empty);
            Assert.That(response.Response, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Generate_Answer()
    {
        var client = Helpers.CreateClient();
        var response = await client.Lemur.QuestionAnswerAsync(new LemurQuestionAnswerParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
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
        Assert.Multiple(() =>
        {
            Assert.That(response.RequestId, Is.Not.Empty);
            Assert.That(response.Response, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(response.Response.First().Question, Is.Not.Empty);
            Assert.That(response.Response.First().Answer, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Generate_Action_Items()
    {
        var client = Helpers.CreateClient();
        var response = await client.Lemur.ActionItemsAsync(new LemurActionItemsParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = TranscriptIds
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response.RequestId, Is.Not.Empty);
            Assert.That(response.Response, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Should_Generate_Task()
    {
        var client = Helpers.CreateClient();
        var response = await client.Lemur.TaskAsync(new LemurTaskParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = TranscriptIds,
            Prompt = "Write a haiku about this conversation."
        }).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response.RequestId, Is.Not.Empty);
            Assert.That(response.Response, Is.Not.Empty);
        });
    }

    [Test]
    // [Ignore("Ignore until fixed")]
    public void Should_Fail_To_Generate_Summary()
    {
        var client = Helpers.CreateClient();
        var ex = Assert.ThrowsAsync<ApiException>(async () => await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = ["bad-id"],
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false));

        Assert.That(ex.Message, Is.EqualTo("each transcript source id must be valid"));
    }

    [Test]
    public async Task Should_Return_Response()
    {
        var client = Helpers.CreateClient();
        var taskResponse = await client.Lemur.TaskAsync(new LemurTaskParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = TranscriptIds,
            Prompt = "Write a haiku about this conversation."
        }).ConfigureAwait(false);

        await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

        var taskResponse2OneOf = await client.Lemur.GetResponseAsync(taskResponse.RequestId).ConfigureAwait(false);
        var taskResponse2 = taskResponse2OneOf.AsT0;
        Assert.Multiple(() =>
        {
            Assert.That(taskResponse2.RequestId, Is.EqualTo(taskResponse.RequestId));
            Assert.That(taskResponse2.Response, Is.EqualTo(taskResponse.Response));
        });

        var qaResponse = await client.Lemur.QuestionAnswerAsync(new LemurQuestionAnswerParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
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

        await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

        var qaResponse2OneOf = await client.Lemur.GetResponseAsync(qaResponse.RequestId).ConfigureAwait(false);
        var qaResponse2 = qaResponse2OneOf.AsT1;
        Assert.Multiple(() =>
        {
            Assert.That(qaResponse2.RequestId, Is.EqualTo(qaResponse.RequestId));
            Assert.That(qaResponse2.Response.Count(), Is.EqualTo(qaResponse.Response.Count()));
        });
    }

    [Test]
    public async Task Should_Purge_Request_Data()
    {
        var client = Helpers.CreateClient();
        var summaryResponse = await client.Lemur.SummaryAsync(new LemurSummaryParams
        {
            FinalModel = LemurModel.AnthropicClaude3_Haiku,
            TranscriptIds = TranscriptIds,
            AnswerFormat = "one sentence"
        }).ConfigureAwait(false);

        await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

        var deletionRequest = await client.Lemur.PurgeRequestDataAsync(summaryResponse.RequestId).ConfigureAwait(false);
        Assert.Multiple(() =>
        {
            Assert.That(deletionRequest.Deleted, Is.True);
            Assert.That(summaryResponse.RequestId, Is.EqualTo(deletionRequest.RequestIdToPurge));
        });
    }
}