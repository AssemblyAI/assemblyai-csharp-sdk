﻿// ReSharper disable All
#pragma warning disable
namespace AssemblyAI.Realtime.WebsocketClient.Exceptions;

/// <summary>
/// Custom exception related to WebsocketClient
/// </summary>
public class WebsocketException : Exception
{
    /// <inheritdoc />
    public WebsocketException()
    {
        }

    /// <inheritdoc />
    public WebsocketException(string message)
        : base(message)
    {
        }

    /// <inheritdoc />
    public WebsocketException(string message, Exception innerException) : base(message, innerException)
    {
        }
}