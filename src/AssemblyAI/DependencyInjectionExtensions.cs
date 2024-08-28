#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AssemblyAI;

/// <summary>
/// Extensions to add the AssemblyAI client to the service collection.
/// </summary>
public static class DependencyInjectionExtensions
{
    private const string AssemblyAIHttpClientName = "AssemblyAI";

    /// <summary>
    /// Add the AssemblyAI client to the service collection.
    /// The AssemblyAI options are configured using the "AssemblyAI" section.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddAssemblyAIClient(this IServiceCollection services)
    {
        var optionsBuilder = services.AddOptions<ClientOptions>();
        optionsBuilder.BindConfiguration("AssemblyAI");
        Validate(optionsBuilder);
        AddServices(services);
        return services;
    }

    /// <summary>
    /// Add the AssemblyAI client to the service collection.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="namedConfigurationSection">The section where the AssemblyAI options are configured.</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddAssemblyAIClient(
        this IServiceCollection services,
        IConfiguration namedConfigurationSection
    )
    {
        var optionsBuilder = services.AddOptions<ClientOptions>();
        optionsBuilder.Bind(namedConfigurationSection);
        Validate(optionsBuilder);
        AddServices(services);
        return services;
    }

    /// <summary>
    /// Add the AssemblyAI client to the service collection.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configureOptions">Configure the client options using this callback.</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddAssemblyAIClient(
        this IServiceCollection services,
        Action<ClientOptions> configureOptions
    )
        => AddAssemblyAIClient(services, (_, options) => configureOptions(options));

    /// <inheritdoc cref="AddAssemblyAIClient(IServiceCollection,Action{ClientOptions})"/>
    public static IServiceCollection AddAssemblyAIClient(
        this IServiceCollection services,
        Action<IServiceProvider, ClientOptions> configureOptions
    )
    {
        var optionsBuilder = services.AddOptions<ClientOptions>();
        optionsBuilder.Configure<IServiceProvider>((options, provider) => configureOptions(provider, options));
        Validate(optionsBuilder);
        AddServices(services);
        return services;
    }

    /// <summary>
    /// Add the AssemblyAI client to the service collection.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="options">The AssemblyAI client options.</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddAssemblyAIClient(
        this IServiceCollection services,
        ClientOptions options
    )
    {
        var optionsBuilder = services.AddOptions<ClientOptions>();

        optionsBuilder.Configure<IServiceProvider>((optionsToConfigure, _) =>
        {
            optionsToConfigure.ApiKey = options.ApiKey;
            optionsToConfigure.HttpClient = options.HttpClient;
            optionsToConfigure.BaseUrl = options.BaseUrl;
            optionsToConfigure.MaxRetries = options.MaxRetries;
            optionsToConfigure.Timeout = options.Timeout;
        });
        Validate(optionsBuilder);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddHttpClient(AssemblyAIHttpClientName);
        services.AddTransient(CreateAssemblyAIClient);
    }

    private static void Validate(OptionsBuilder<ClientOptions> optionsBuilder)
    {
        optionsBuilder.Validate(options => !string.IsNullOrEmpty(options.ApiKey), 
            "AssemblyAI:ApiKey is required."
        );
    }
    private static AssemblyAIClient CreateAssemblyAIClient(IServiceProvider provider)
    {
        var options = provider.GetRequiredService<IOptionsSnapshot<ClientOptions>>().Value;
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        options.HttpClient ??= provider.GetRequiredService<IHttpClientFactory>().CreateClient(AssemblyAIHttpClientName);
        return new AssemblyAIClient(options);
    }
}
#endif