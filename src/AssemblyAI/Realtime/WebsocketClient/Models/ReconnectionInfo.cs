﻿// ReSharper disable All
#pragma warning disable
namespace Websocket.Client;

/// <summary>
/// Info about happened reconnection
/// </summary>
internal class ReconnectionInfo
{
    /// <summary>
    /// Info about happened reconnection
    /// </summary>
    public ReconnectionInfo(ReconnectionType type)
    {
            Type = type;
        }

    /// <summary>
    /// Reconnection reason
    /// </summary>
    public ReconnectionType Type { get; }

    /// <summary>
    /// Simple factory method
    /// </summary>
    public static ReconnectionInfo Create(ReconnectionType type)
    {
            return new ReconnectionInfo(type);
        }
}