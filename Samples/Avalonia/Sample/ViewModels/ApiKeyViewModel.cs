using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Sample.ViewModels;

public class ApiKeyViewModel(NavigationViewModel navigationViewModel) : NavigateViewModelBase(navigationViewModel)
{
    private readonly ApiKeyContainer _apiKeyContainer = DiContainer.Services.GetRequiredService<ApiKeyContainer>();
    
    private string _apiKey = "";

    public string ApiKey
    {
        get => _apiKey;
        set => this.RaiseAndSetIfChanged(ref _apiKey, value);
    }

    public void Save()
    {
        _apiKeyContainer.ApiKey = ApiKey;
        Navigate(new TabsViewModel());
    }
}