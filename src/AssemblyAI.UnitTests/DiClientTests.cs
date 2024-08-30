#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class DiClientTests
{
    [Test]
    public void AssemblyAIClient_Should_Be_Correctly_Configured_From_DI()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AssemblyAI:ApiKey", "test_api_key" },
                { "AssemblyAI:BaseUrl", "https://api.test.assemblyai.com" },
                { "AssemblyAI:MaxRetries", "3" },
                { "AssemblyAI:TimeoutInSeconds", "60" }
            }!)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddAssemblyAIClient();

        // Act
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<AssemblyAIClient>();

        // Assert
        Assert.That(client, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(client.Files, Is.Not.Null);
            Assert.That(client.Transcripts, Is.Not.Null);
            Assert.That(client.Realtime, Is.Not.Null);
            Assert.That(client.Lemur, Is.Not.Null);
        });
    }

    [Test]
    public void AssemblyAIClient_Throws_Exception_When_Configuration_Missing()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build(); // Empty configuration

        services.AddSingleton<IConfiguration>(configuration);
        services.AddAssemblyAIClient();

        var serviceProvider = services.BuildServiceProvider();

        var exception = Assert.Throws<OptionsValidationException>(() => serviceProvider.GetService<AssemblyAIClient>());
        Assert.That(exception.Message, Is.EqualTo("AssemblyAI:ApiKey is required."));
    }
}
#endif