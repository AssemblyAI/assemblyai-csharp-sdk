using System;

#nullable enable

namespace AssemblyAI.Core;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class AssemblyAIClientException(string message, Exception? innerException = null)
    : Exception(message, innerException) { }
