using System;

#nullable enable

namespace AssemblyAI;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class AssemblyAIException(string message, Exception? innerException = null)
    : Exception(message, innerException) { }
