using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TCoSServer.Common;
using TCoSServer.GameServer.Network;
using TCoSServer.GameServer.Network.Packets;

namespace TCoSServer.GameServer
{

  /// <summary>
  /// Entry point for the GameServer.
  /// Accepts new connection and then give 
  /// that connection to NetworkPlayer.
  /// </summary>
  class GameServer
  {
    //Temp cheat
    public static bool BypassCharacterScreen = false;

    private IPEndPoint localEndPoint;
    private Socket listener;
    private ManualResetEvent listenForNewConnection = new ManualResetEvent (false);
    private bool valid;

    public bool isValid
    {
      get {return valid;}
    }

    List<NetworkPlayer> playersList;

    public void DebugSendMessageToEveryone (SBPacket message)
    {
      foreach (NetworkPlayer player in playersList)
        player.SendMessage (message);
    }

    public GameServer (string ipAddress, string port)
    {
      valid = true;
      try
      {
        IPAddress serverIp = IPAddress.Parse (ipAddress);
        Int32 lport = Int32.Parse (port);
        localEndPoint = new IPEndPoint (serverIp, lport);

        playersList = new List<NetworkPlayer> (20);
      }
      catch (FormatException)
      {
        valid = false;
      }

      //Init everything
      NetworkPlayer.InitMessageHandlers ();
    }

    public void StopServer ()
    {
      listener.Close ();
      Console.WriteLine ("Game server stopped");
    }

    public void StartServer ()
    {
       if (!isValid)
          throw new Exception ("Login server is in a invalid state");

        // Create a TCP/IP socket.
        listener = new Socket (AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
          listener.Bind (localEndPoint);
          listener.Listen (100);

          while (true)
          {
            if (Thread.CurrentThread.ThreadState == System.Threading.ThreadState.StopRequested)
              break;

            listenForNewConnection.Reset ();

            // Start an asynchronous socket to listen for connections.
            Console.WriteLine ("Waiting for a connection...");
            listener.BeginAccept (
                new AsyncCallback (AcceptNextGameSession),
                 listener);

            // Wait until a connection is made before continuing.
            listenForNewConnection.WaitOne ();
          }
        }
      catch(Exception e)
      {
        Console.WriteLine (e.ToString ());
      }
    }

    private void AcceptNextGameSession (IAsyncResult ar)
    {
      // Signal the main thread to continue.
      listenForNewConnection.Set ();

      Socket listener = (Socket)ar.AsyncState;
      Socket sessionHandler = null;
      try
      {
        sessionHandler = listener.EndAccept (ar);
        Console.WriteLine ("GS: New connection from {0}", sessionHandler.RemoteEndPoint);
        Message message = new Message ();
        message.clientSocket = sessionHandler;
        sessionHandler.BeginReceive (message.header, 0, Message.headerSize, SocketFlags.None, 
                                      new AsyncCallback (HandleConnect), message);

      }
      catch (ObjectDisposedException)
      {
        return;
      }
    }

    private void HandleConnect (IAsyncResult ar)
    {
      Message firstMessage = (Message)ar.AsyncState;
      Socket handler = firstMessage.clientSocket;
      int bytesRead = handler.EndReceive (ar);

      using (MessageReader reader = new MessageReader (firstMessage))
      {
        handler.Receive (firstMessage.data, firstMessage.size, SocketFlags.None);
        if (firstMessage.id != (ushort)Network.GameMessageIds.CONNECT)
        {
          Console.WriteLine ("Connection started with another packet than CONNECT. Terminate connection");
          firstMessage.clientSocket.Close ();
          return;
        }

        Message nextMessage = new Message ();
        nextMessage.clientSocket = handler;
        handler.BeginReceive (nextMessage.header, 0, Message.headerSize, SocketFlags.None,
                                      new AsyncCallback (HandleTravelConnect), nextMessage);
      }
    }

    private void HandleTravelConnect (IAsyncResult ar)
    {
      Message message = (Message)ar.AsyncState;
      Socket handler = message.clientSocket;
      int bytesRead = handler.EndReceive (ar);
      using (MessageReader reader = new MessageReader (message))
      {
        handler.Receive (message.data, message.size, SocketFlags.None);
        Console.WriteLine ("Message ID: " + message.id);
        if (message.id != (ushort)GameMessageIds.C2S_TRAVEL_CONNECT)
        {
          Console.WriteLine ("Connection started with another packet than C2S_TRAVEL_CONNECT. Terminate connection");
          handler.Close ();
          return;
        }
        uint transportKey = reader.ReadUInt32 ();
        NetworkPlayer player = new NetworkPlayer (message.clientSocket, transportKey, ref playersList);
        playersList.Add (player);
      }
    }
  }
}
