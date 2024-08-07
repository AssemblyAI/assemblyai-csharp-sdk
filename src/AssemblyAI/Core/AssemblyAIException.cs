using System;

#nullable enable

namespace AssemblyAI.Core;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class AssemblyAIException : Exception
{
    public AssemblyAIException()
    {
    }

    public AssemblyAIException(string message) : base(message)
    {
    }

    public AssemblyAIException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
