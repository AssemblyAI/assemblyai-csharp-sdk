using Avalonia.Controls;
using Avalonia.Input;

namespace Sample.Views;

public partial class TranscribeFileView : UserControl
{
    public TranscribeFileView()
    {
        InitializeComponent();
    }

    private void LemurPromptKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        
        e.Handled = true;
        AskQuestionButton.Command?.Execute(null);
    }
}