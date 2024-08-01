using ReactiveUI;

namespace Sample.ViewModels;

public class LemurMessageViewModel(string message, LemurMessageType type) : ReactiveObject
{
    public LemurMessageType Type
    {
        get => type;
        set => this.RaiseAndSetIfChangedMultiple(
            ref type,
            value,
            nameof(Type),
            nameof(IsUserMessage),
            nameof(IsAssistantMessage)
        );
    }

    public bool IsUserMessage => Type == LemurMessageType.User;
    public bool IsAssistantMessage => Type == LemurMessageType.Assistant;

    public string Message
    {
        get => message;
        set => this.RaiseAndSetIfChanged(ref message, value);
    }

    public static LemurMessageViewModel FromUser(string message)
        => new(message, LemurMessageType.User);

    public static LemurMessageViewModel FromAssistant(string message)
        => new(message, LemurMessageType.Assistant);
}

public enum LemurMessageType
{
    Assistant,
    User
}