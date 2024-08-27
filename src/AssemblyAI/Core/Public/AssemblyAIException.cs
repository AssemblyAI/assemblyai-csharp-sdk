// ReSharper disable CheckNamespace
namespace AssemblyAI;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class AssemblyAIException : Exception
{
    internal AssemblyAIException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
