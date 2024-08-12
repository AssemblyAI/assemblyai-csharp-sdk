namespace AssemblyAI;

/// <summary>	
/// Wraps an event that can be subscribed to and can be invoked	
/// </summary>	
/// <typeparam name="T"></typeparam>	
public class Event<T> : IDisposable
{
    private readonly HashSet<Action<T>> _subscribers = [];
    private readonly HashSet<Func<T, Task>> _subscribersAsync = [];

    internal Event()
    {
    }

    internal async Task RaiseEvent(T eventObject)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(eventObject);
        }

        foreach (var subscriber in _subscribersAsync)
        {
            await subscriber.Invoke(eventObject).ConfigureAwait(false);
        }
    }

    public void Subscribe(Action<T> eventHandler)
    {
        _subscribers.Add(eventHandler);
    }

    public void Subscribe(Func<T, Task> eventHandler)
    {
        _subscribersAsync.Add(eventHandler);
    }

    public void Unsubscribe(Action<T> eventHandler)
    {
        if (_subscribers.Contains(eventHandler))
        {
            _subscribers.Remove(eventHandler);
        }
    }

    public void Unsubscribe(Func<T, Task> eventHandler)
    {
        if (_subscribersAsync.Contains(eventHandler))
        {
            _subscribersAsync.Remove(eventHandler);
        }
    }

    public void UnsubscribeAll()
    {
        _subscribers.Clear();
        _subscribersAsync.Clear();
    }

    public void Dispose() => UnsubscribeAll();
}