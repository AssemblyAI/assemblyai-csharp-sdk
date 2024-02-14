using System;
using AssemblyAI;
using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Sample;

namespace Sample.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        DiContainer.BuildServices((services) =>
        {
            services.AddTransient<ICaptureAudio, CaptureAudio>();
            services.AddSingleton<ApiKeyContainer>();
            services.AddTransient<AssemblyAIClient>(services =>
            {
                var apiContainer = services.GetRequiredService<ApiKeyContainer>();
                return new AssemblyAIClient(apiContainer.ApiKey);
            });
        });
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}