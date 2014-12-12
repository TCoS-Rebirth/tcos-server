using System.Collections;
using System.Net.Sockets;
using TCoSServer.Common;
using System.Threading.Tasks;
using System;

namespace TCoSServer.GameServer.Network
{
  /// <summary>
  /// Represents a connection to a client.
  /// Handle all the sending/receive stuff and ensure
  /// that packets are sended without being mixed together.
  /// Internally uses a queue storing all the messages to send.
  /// </summary>
  class Connection
  {
    private Queue messagesToSend;
    private Socket connectionToClient;
    private bool isSending;
    private bool mustClose;

    public Connection (Socket clientSocket)
    {
      connectionToClient = clientSocket;
      messagesToSend = new Queue ();
      messagesToSend = Queue.Synchronized (messagesToSend);

      isSending = false;
      mustClose = false;
    }

    public void Close()
    {
      if (isSending)
        mustClose = true;
      else
        connectionToClient.Disconnect (false);
    }

    public async void Send (Message toSend)
    {
      Action action = SendAsyncInternal;
      messagesToSend.Enqueue (toSend);
      if (!isSending)
        await Task.Run (action);
    }

    private void SendAsyncInternal ()
    {
      isSending = true;
      while (true)
      {
        try
        {
          Message toSend = (Message)messagesToSend.Dequeue ();
          connectionToClient.Send (toSend.data);
        }
        catch (InvalidOperationException)
        {
          break;
        }
        catch (SocketException)
        {
          break;
        }
      }
      isSending = false;
      if (mustClose)
        connectionToClient.Disconnect (false);
    }
  }
}
