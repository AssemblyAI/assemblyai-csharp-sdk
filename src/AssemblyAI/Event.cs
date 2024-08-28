// ReSharper disable UnusedMember.Global
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

    /// <summary>
    /// Subscribe to the event
    /// </summary>
    public void Subscribe(Action<T> eventHandler)
    {
        _subscribers.Add(eventHandler);
    }

    /// <inheritdoc cref="Subscribe(System.Action{T})"/>
    public void Subscribe(Func<T, Task> eventHandler)
    {
        _subscribersAsync.Add(eventHandler);
    }

    /// <summary>
    /// Unsubscribe from the event
    /// </summary>
    /// <param name="eventHandler"></param>
    public void Unsubscribe(Action<T> eventHandler)
    {
        if (_subscribers.Contains(eventHandler))
        {
            _subscribers.Remove(eventHandler);
        }
    }

    /// <inheritdoc cref="Unsubscribe(System.Action{T})"/>
    public void Unsubscribe(Func<T, Task> eventHandler)
    {
        if (_subscribersAsync.Contains(eventHandler))
        {
            _subscribersAsync.Remove(eventHandler);
        }
    }

    /// <summary>
    /// Unsubscribe all event handlers
    /// </summary>
    public void UnsubscribeAll()
    {
        _subscribers.Clear();
        _subscribersAsync.Clear();
    }
    
    /// <summary>
    /// Dispose of the event.
    /// Unsubscribes all event handlers.
    /// </summary>
    public void Dispose() => UnsubscribeAll();
}