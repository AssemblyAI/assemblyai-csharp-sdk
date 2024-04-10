using ReactiveUI;

namespace Sample.ViewModels;

public class NavigationViewModel : ReactiveObject
{
    public NavigationViewModel()
    {
        _contentViewModel = new ApiKeyViewModel(this);
    }
    
    private ReactiveObject _contentViewModel;

    public ReactiveObject ContentViewModel
    {
        get => _contentViewModel;
        set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
}