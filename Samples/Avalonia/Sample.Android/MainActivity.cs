using System.Linq;
using Android.App;
using Android.Content.PM;
using AssemblyAI;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using Java.Nio;
using Microsoft.Extensions.DependencyInjection;
using Sample;
using Application = Android.App.Application;

namespace Sample.Android;

[Activity(
    Label = "Sample.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        DiContainer.BuildServices(services =>
        {
            services.AddTransient<ICaptureAudio, CaptureAudio>(_ => new CaptureAudio(this));
            services.AddSingleton<ApiKeyContainer>();
            services.AddTransient<AssemblyAIClient>(services =>
            {
                var apiContainer = services.GetRequiredService<ApiKeyContainer>();
                return new AssemblyAIClient(apiContainer.ApiKey);
            });
        });
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }
}