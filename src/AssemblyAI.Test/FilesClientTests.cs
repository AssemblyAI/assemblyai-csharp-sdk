using NUnit.Framework;

namespace AssemblyAI.Test;

[TestFixture]
public class FilesClientTests
{
    private string _apiKey;

    [SetUp]
    public void Setup()
    {
        // Retrieve the API key from the .runsettings file
        _apiKey = TestContext.Parameters.Get("ASSEMBLYAI_API_KEY");
        if(string.IsNullOrEmpty(_apiKey)) throw new Exception("ASSEMBLYAI_API_KEY .runsetting parameter is not set.");
    }
    
    [Test]
    public async Task Should_Upload_File_Using_FileInfo()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileInfo = new FileInfo(testFilePath);

        var uploadedFile = await client.Files.UploadAsync(fileInfo).ConfigureAwait(false);

        Assert.That(uploadedFile, Is.Not.Null);
        Assert.That(uploadedFile.UploadUrl, Is.Not.Null);
    }
    
    [Test]
    public async Task Should_Upload_File_Using_Stream()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = new AssemblyAIClient(_apiKey);

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        await using var fileStream = File.OpenRead(testFilePath);

        var uploadedFile = await client.Files.UploadAsync(fileStream).ConfigureAwait(false);

        Assert.That(uploadedFile, Is.Not.Null);
        Assert.That(uploadedFile.UploadUrl, Is.Not.Null);
    }
}