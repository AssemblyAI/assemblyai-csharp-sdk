using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Sample.ViewModels;
using Sample.Views;

namespace Sample;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ActivatorUtilities.CreateInstance<NavigationViewModel>(DiContainer.Services)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = ActivatorUtilities.CreateInstance<NavigationViewModel>(DiContainer.Services)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}