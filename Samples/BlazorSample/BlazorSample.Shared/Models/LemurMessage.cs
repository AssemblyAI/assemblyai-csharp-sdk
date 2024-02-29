using ReactiveUI;

namespace BlazorSample.Shared.Models;

public class LemurMessage(string message, LemurMessageType type) : ReactiveObject
{
    public LemurMessageType Type { get; set; } = type;
    public bool IsUserMessage => Type == LemurMessageType.User;
    public bool IsAssistantMessage => Type == LemurMessageType.Assistant;
    public string Message { get; set; } = message;

    public static LemurMessage FromUser(string message)
        => new(message, LemurMessageType.User);
    public static LemurMessage FromAssistant(string message)
        => new(message, LemurMessageType.Assistant);
}

public enum LemurMessageType
{
    Assistant,
    User
}