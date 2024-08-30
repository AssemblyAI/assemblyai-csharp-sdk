namespace AssemblyAI.IntegrationTests;

[TestFixture]
public class FilesClientTests
{
    [Test]
    public async Task Should_Upload_File_Using_FileInfo()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = Helpers.CreateClient();

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
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        using var fileStream = File.OpenRead(testFilePath);

        var uploadedFile = await client.Files.UploadAsync(fileStream).ConfigureAwait(false);

        Assert.That(uploadedFile, Is.Not.Null);
        Assert.That(uploadedFile.UploadUrl, Is.Not.Null);
    }
    
    [Test]
    public async Task Should_Upload_File_Using_Stream_With_Dispose()
    {
        // Assuming there's a method to create a configured RawClient instance
        var client = Helpers.CreateClient();

        // Adjust the path to where your test file is located
        var testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "nbc.mp3");
        var fileStream = File.OpenRead(testFilePath);

        var uploadedFile = await client.Files.UploadAsync(fileStream, disposeStream: true).ConfigureAwait(false);

        Assert.That(uploadedFile, Is.Not.Null);
        Assert.That(uploadedFile.UploadUrl, Is.Not.Null);
        Assert.Throws<ObjectDisposedException>(() => fileStream.ReadByte());
    }
}