#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
using AssemblyAI.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AssemblyAI;

public static class DependencyInjectionExtensions
{
    private const string AssemblyAIHttpClientName = "AssemblyAI";

    public static IServiceCollection AddAssemblyAIClient(this IServiceCollection services)
    {
        var optionsBuilder = services.AddOptions<ClientOptions>();
        optionsBuilder.BindConfiguration("AssemblyAI");
        Validate(optionsBuilder);
        AddServices(services);
        return services;
    }

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

    public static IServiceCollection AddAssemblyAIClient(
        this IServiceCollection services,
        Action<ClientOptions> configureOptions
    )
        => AddAssemblyAIClient(services, (_, options) => configureOptions(options));


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