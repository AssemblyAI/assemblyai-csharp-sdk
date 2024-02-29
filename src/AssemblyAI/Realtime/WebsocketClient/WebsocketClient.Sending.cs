﻿using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;

namespace AssemblyAI.Realtime.WebsocketClient;

internal partial class WebsocketClient
{
    private readonly Channel<RequestMessage> _messagesTextToSendQueue = Channel.CreateUnbounded<RequestMessage>(
        new UnboundedChannelOptions()
        {
            SingleReader = true,
            SingleWriter = false
        });

    private readonly Channel<ArraySegment<byte>> _messagesBinaryToSendQueue =
        Channel.CreateUnbounded<ArraySegment<byte>>(new UnboundedChannelOptions()
        {
            SingleReader = true,
            SingleWriter = false
        });


    /// <summary>
    /// Send text message to the websocket channel. 
    /// It inserts the message to the queue and actual sending is done on another thread
    /// </summary>
    /// <param name="message">Text message to be sent</param>
    /// <returns>true if the message was written to the queue</returns>
    public bool Send(string message)
    {
            return _messagesTextToSendQueue.Writer.TryWrite(new RequestTextMessage(message));
        }

    /// <summary>
    /// Send binary message to the websocket channel. 
    /// It inserts the message to the queue and actual sending is done on another thread
    /// </summary>
    /// <param name="message">Binary message to be sent</param>
    /// <returns>true if the message was written to the queue</returns>
    public bool Send(byte[] message)
    {
            return _messagesBinaryToSendQueue.Writer.TryWrite(new ArraySegment<byte>(message));
        }

    /// <summary>
    /// Send binary message to the websocket channel. 
    /// It inserts the message to the queue and actual sending is done on another thread
    /// </summary>
    /// <param name="message">Binary message to be sent</param>
    /// <returns>true if the message was written to the queue</returns>
    public bool Send(ArraySegment<byte> message)
    {
            return _messagesBinaryToSendQueue.Writer.TryWrite(message);
        }

    /// <summary>
    /// Send text message to the websocket channel. 
    /// It doesn't use a sending queue, 
    /// beware of issue while sending two messages in the exact same time 
    /// on the full .NET Framework platform
    /// </summary>
    /// <param name="message">Message to be sent</param>
    public Task SendInstant(string message)
    {
            return SendInternalSynchronized(new RequestTextMessage(message));
        }

    /// <summary>
    /// Send binary message to the websocket channel. 
    /// It doesn't use a sending queue, 
    /// beware of issue while sending two messages in the exact same time 
    /// on the full .NET Framework platform
    /// </summary>
    /// <param name="message">Message to be sent</param>
    public Task SendInstant(byte[] message)
    {
            return SendInternalSynchronized(new ArraySegment<byte>(message));
        }

    /// <summary>
    /// Send already converted text message to the websocket channel. 
    /// Use this method to avoid double serialization of the text message.
    /// It inserts the message to the queue and actual sending is done on another thread
    /// </summary>
    /// <param name="message">Message to be sent</param>
    /// <returns>true if the message was written to the queue</returns>
    public bool SendAsText(byte[] message)
    {
            return _messagesTextToSendQueue.Writer.TryWrite(new RequestBinaryMessage(message));
        }

    /// <summary>
    /// Send already converted text message to the websocket channel. 
    /// Use this method to avoid double serialization of the text message.
    /// It inserts the message to the queue and actual sending is done on another thread
    /// </summary>
    /// <param name="message">Message to be sent</param>
    /// <returns>true if the message was written to the queue</returns>
    public bool SendAsText(ArraySegment<byte> message)
    {
            return _messagesTextToSendQueue.Writer.TryWrite(new RequestBinarySegmentMessage(message));
        }

    private async Task SendTextFromQueue()
    {
            try
            {
                while (await _messagesTextToSendQueue.Reader.WaitToReadAsync())
                {
                    while (_messagesTextToSendQueue.Reader.TryRead(out var message))
                    {
                        await SendInternalSynchronized(message).ConfigureAwait(false);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // task was canceled, ignore
            }
            catch (OperationCanceledException)
            {
                // operation was canceled, ignore
            }
            catch (Exception)
            {
                if (_cancellationTotal?.IsCancellationRequested == true || _disposing)
                {
                    // disposing/canceling, do nothing and exit
                    return;
                }

                StartBackgroundThreadForSendingText();
            }
        }

    private async Task SendBinaryFromQueue()
    {
            try
            {
                while (await _messagesBinaryToSendQueue.Reader.WaitToReadAsync())
                {
                    while (_messagesBinaryToSendQueue.Reader.TryRead(out var message))
                    {
                        await SendInternalSynchronized(message).ConfigureAwait(false);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // task was canceled, ignore
            }
            catch (OperationCanceledException)
            {
                // operation was canceled, ignore
            }
            catch (Exception)
            {
                if (_cancellationTotal?.IsCancellationRequested == true || _disposing)
                {
                    // disposing/canceling, do nothing and exit
                    return;
                }

                StartBackgroundThreadForSendingBinary();
            }
        }

    private void StartBackgroundThreadForSendingText()
    {
            _ = Task.Factory.StartNew(_ => SendTextFromQueue(), TaskCreationOptions.LongRunning,
                _cancellationTotal?.Token ?? CancellationToken.None);
        }

    private void StartBackgroundThreadForSendingBinary()
    {
            _ = Task.Factory.StartNew(_ => SendBinaryFromQueue(), TaskCreationOptions.LongRunning,
                _cancellationTotal?.Token ?? CancellationToken.None);
        }

    private async Task SendInternalSynchronized(RequestMessage message)
    {
            using (await _locker.LockAsync())
            {
                await SendInternal(message);
            }
        }

    private async Task SendInternal(RequestMessage message)
    {
            if (!IsClientConnected())
            {
                return;
            }

            ArraySegment<byte> payload;

            switch (message)
            {
                case RequestTextMessage textMessage:
                    payload = new ArraySegment<byte>(Encoding.UTF8.GetBytes(textMessage.Text));
                    break;
                case RequestBinaryMessage binaryMessage:
                    payload = new ArraySegment<byte>(binaryMessage.Data);
                    break;
                case RequestBinarySegmentMessage segmentMessage:
                    payload = segmentMessage.Data;
                    break;
                default:
                    throw new ArgumentException($"Unknown message type: {message.GetType()}");
            }

            await _client
                .SendAsync(payload, WebSocketMessageType.Text, true, _cancellation?.Token ?? CancellationToken.None)
                .ConfigureAwait(false);
        }

    private async Task SendInternalSynchronized(ArraySegment<byte> message)
    {
            using (await _locker.LockAsync())
            {
                await SendInternal(message);
            }
        }

    private async Task SendInternal(ArraySegment<byte> payload)
    {
            if (!IsClientConnected())
            {
                return;
            }

            await _client
                .SendAsync(payload, WebSocketMessageType.Binary, true, _cancellation?.Token ?? CancellationToken.None)
                .ConfigureAwait(false);
        }
}