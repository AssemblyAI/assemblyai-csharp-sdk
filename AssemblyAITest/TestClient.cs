using AssemblyAI.Core;

namespace AssemblyAITest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestClient()
    {
        var aai = new AssemblyAI.AssemblyAI("ey...");
        var transcript = await aai.Transcript.Get("transcript-id");
    }
}