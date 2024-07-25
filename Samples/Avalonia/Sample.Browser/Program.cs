using System.Runtime.Versioning;
using System.Threading.Tasks;
using AssemblyAI;
using AssemblyAI.Core;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Sample;
using Sample;
using Sample.Browser;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        DiContainer.BuildServices((services) =>
        {
            services.AddTransient<ICaptureAudio, CaptureAudio>();
            services.AddSingleton<ApiKeyContainer>();
            services.AddAssemblyAIClient((services, options) =>
            {
                var apiContainer = services.GetRequiredService<ApiKeyContainer>();
                options.ApiKey = apiContainer.ApiKey;
                options.BaseUrl = "https://localhost:7030/api";
            });
        });
        return BuildAvaloniaApp()
            .WithInterFont()
            .UseReactiveUI()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}