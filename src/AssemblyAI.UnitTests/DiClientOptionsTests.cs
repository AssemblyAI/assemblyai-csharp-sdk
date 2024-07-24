using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class DependencyInjectionClientOptionsTests
{
    private static readonly ClientOptions ValidAssemblyAIOptions = new()
    {
        ApiKey = "MyAssemblyAIApiKey",
        BaseUrl = "https://api.assemblyai.com",
        MaxRetries = 2,
        TimeoutInSeconds = 30
    };

    [Test]
    public void AddAssemblyAIClient_With_Callback_Should_Match_Configuration()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(BuildEmptyConfiguration());
        serviceCollection.AddAssemblyAIClient((_, options) =>
        {
            options.ApiKey = ValidAssemblyAIOptions.ApiKey;
            options.BaseUrl = ValidAssemblyAIOptions.BaseUrl;
            options.MaxRetries = ValidAssemblyAIOptions.MaxRetries;
            options.TimeoutInSeconds = ValidAssemblyAIOptions.TimeoutInSeconds;
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var assemblyAIClientOptions = serviceProvider.GetService<IOptions<ClientOptions>>()?.Value;

        var expectedJson = JsonSerializer.Serialize(ValidAssemblyAIOptions);
        var actualJson = JsonSerializer.Serialize(assemblyAIClientOptions);

        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public async Task AddAssemblyAIClient_From_Configuration_Should_Reload_On_Change()
    {
        const string optionsFile = "ClientOptions.json";
        if (File.Exists(optionsFile)) File.Delete(optionsFile);
        var jsonText = JsonSerializer.Serialize(new { AssemblyAI = ValidAssemblyAIOptions });
        await File.WriteAllTextAsync(optionsFile, jsonText);

        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(optionsFile, optional: false, reloadOnChange: true)
            .Build();

        serviceCollection.AddSingleton<IConfiguration>(configuration);
        serviceCollection.AddAssemblyAIClient();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        ClientOptions updatedOptions = new()
        {
            ApiKey = "UpdatedApiKey",
            BaseUrl = "https://api.updated.assemblyai.com",
            MaxRetries = 3,
            TimeoutInSeconds = 45
        };

        jsonText = JsonSerializer.Serialize(new { AssemblyAI = updatedOptions });
        await File.WriteAllTextAsync(optionsFile, jsonText);

        // Simulate waiting for the option change to be detected
        await Task.Delay(1000); // This is a simplification. In real tests, use a more reliable method to wait for changes.

        var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<ClientOptions>>();
        var options = monitor.CurrentValue;

        var expectedJson = JsonSerializer.Serialize(updatedOptions);
        var actualJson = JsonSerializer.Serialize(options);
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    private static IConfiguration BuildEmptyConfiguration() => new ConfigurationBuilder().Build();
}