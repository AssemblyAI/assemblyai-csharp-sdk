using ReactiveUI;

namespace Sample.ViewModels;

public class TabsViewModel : ViewModelBase
{
    private TranscribeFileViewModel _transcribeFileViewModel = new TranscribeFileViewModel();

    public TranscribeFileViewModel TranscribeFileViewModel
    {
        get => _transcribeFileViewModel;
        set => this.RaiseAndSetIfChanged(ref _transcribeFileViewModel, value);
    }
    
    private TranscribeMicrophoneViewModel _transcribeMicrophoneViewModel = new TranscribeMicrophoneViewModel();

    public TranscribeMicrophoneViewModel TranscribeMicrophoneViewModel
    {
        get => _transcribeMicrophoneViewModel;
        set => this.RaiseAndSetIfChanged(ref _transcribeMicrophoneViewModel, value);
    }
}