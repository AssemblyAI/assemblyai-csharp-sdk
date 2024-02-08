using ReactiveUI;

namespace Sample.ViewModels;

public class NavigateViewModelBase(NavigationViewModel navigationViewModel) : ViewModelBase
{
    public void Navigate(ReactiveObject vm)
    {
        navigationViewModel.ContentViewModel = vm;
    }
}