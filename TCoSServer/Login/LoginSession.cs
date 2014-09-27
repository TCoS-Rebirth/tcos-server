using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using TCoSServer.Common;
using System.IO;
using System.Net;

namespace TCoSServer
{
  namespace Login
  {
    static class LoginSession
    {
      private static AsyncCallback readMessageCallback = new AsyncCallback (ReadMessageCallback);

      private static Dictionary<LoginMessageId, HandleMessageCallback> messageHandlers;

      //Call this method only once, at server initialization
      public static void initMessageHandlers ()
      {
        //Add all message handlers
        messageHandlers = new Dictionary<LoginMessageId, HandleMessageCallback> (5);
        messageHandlers.Add (LoginMessageId.CONNECT, new HandleMessageCallback (HandleConnect));
        messageHandlers.Add (LoginMessageId.DISCONNECT, new HandleMessageCallback (HandleDisconnect));
        messageHandlers.Add (LoginMessageId.C2L_USER_LOGIN, new HandleMessageCallback (HandleUserLogin));
        messageHandlers.Add (LoginMessageId.C2L_QUERY_UNIVERSE_LIST, new HandleMessageCallback (HandleQueryUniverseList));
        messageHandlers.Add (LoginMessageId.C2L_UNIVERSE_SELECTED, new HandleMessageCallback (HandleUniverseSelected));
      }

      public static void StartSession (Socket clientSocket)
      {
        Console.WriteLine ("Start new login session");
        Message header = new Message ();
        header.clientSocket = clientSocket;
        clientSocket.BeginReceive (header.header, 0, Message.headerSize, 0,
              readMessageCallback, header);
      }

      private static void ReadMessageCallback (IAsyncResult ar)
      {
        Message message = (Message)ar.AsyncState;
        Socket handler = message.clientSocket;

        // Read header
        int bytesRead = handler.EndReceive (ar);
        using (MessageReader reader = new MessageReader (message))
        {
          //Read header automatically
        }

        Console.WriteLine ("Received packet number " + message.id + " of size " + message.size);
  

        //Handle message
        messageHandlers[(LoginMessageId)(message.id)] (message);

        if (!handler.Connected)
          return;

        Message nextMessage = new Message ();
        nextMessage.clientSocket = handler;
        //Start listening for next message
        try
        {
          handler.BeginReceive (nextMessage.header, 0, Message.headerSize, 0, readMessageCallback, nextMessage);
        }
        catch (ObjectDisposedException)
        { }
      }

      //Message handlers
      private static void HandleConnect (Message message)
      {
        message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
        using (MessageReader reader = new MessageReader (message))
        {
          UInt32 statusCode = reader.ReadUInt32 ();
          Console.WriteLine ("New connection from {0} with status code {1}", message.clientSocket.RemoteEndPoint.ToString ()
                                                                           , statusCode);
        }
      }

      private static void HandleDisconnect (Message message)
      {
        message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
        using (MessageReader reader = new MessageReader (message))
        {
          uint statusCode = reader.ReadUInt32 ();
          string reason = reader.ReadString ();
          Console.WriteLine ("Disconnected from {0} with status {1} and reason: {2}. ",
            message.clientSocket.RemoteEndPoint.ToString (), statusCode, reason);
        }
        message.clientSocket.Close ();

      }

      private static void HandleUserLogin (Message message)
      {
        Console.WriteLine ("User login...");
        message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
        using (MessageReader reader = new MessageReader (message))
        {
          uint svnVersion = reader.ReadUInt32 ();
          string login = reader.ReadString ();
          string password = reader.ReadString ();
          Console.WriteLine ("SVN version: {0}. User {1}, Password {2}: ", svnVersion, login, password);
        }
        SendUserLoginAck (message.clientSocket);
      }

      private static void HandleQueryUniverseList (Message message)
      {
        Console.WriteLine ("Handle QueryUniverseList");
        SendQueryUniverseListAck (message.clientSocket);
      }

      private static void HandleUniverseSelected (Message message)
      {
        Console.WriteLine ("Universe selected...");
        message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
        using (MessageReader reader = new MessageReader (message))
        {
          uint universeSelectedId = reader.ReadUInt32 ();
          Console.WriteLine ("Selected universe {0}", universeSelectedId);
        }
        SendUniverseSelectedAck (message);
      }

      //Message senders
      private static void SendDisconnected (Socket clientToSend)
      {
        using (MessageWriter writer = new MessageWriter (LoginMessageId.DISCONNECT, 4))
        {
          Message message = writer.ComputeMessage ();
          clientToSend.Send (message.data);
        }
      }

      private static void SendUserLoginAck (Socket clientToSend)
      {
        Message message = null;
        using (MessageWriter writer = new MessageWriter (LoginMessageId.L2C_USER_LOGIN_ACK, 4))
        {
          writer.Write (0);
          writer.Write ((int)eLoginRequestResult.LRR_NONE);
          message = writer.ComputeMessage ();
        }
        clientToSend.Send (message.data);
      }

      private static void SendQueryUniverseListAck (Socket clientToSend)
      {
        //Simulate universe
        const uint statusCode = 0;
        const uint universeNumber = 1;

        const uint universeId = 42;
        const string universeName = "Spellborn Rebirth";
        const string universeLanguage = "English";
        const string universeType = "Roleplay";
        const string universePopulation = "Very low";

        Message universeListAck = null;
        using (MessageWriter writer = new MessageWriter (LoginMessageId.L2C_QUERY_UNIVERSE_LIST_ACK))
        {
          writer.Write (statusCode);
          writer.Write (universeNumber);

          writer.Write (universeId);
          writer.Write (universeName);
          writer.Write (universeLanguage);
          writer.Write (universeType);
          writer.Write (universePopulation);

          universeListAck = writer.ComputeMessage ();
        }
        clientToSend.Send (universeListAck.data);
      }

      private static void SendUniverseSelectedAck (Message universeSelected)
      {
        //Simulate packet with fixed data
        const uint statusCode = 0;
        const uint unknownDword = 0x22222222;
        const string universePackageName = "Complete_Universe";
        const uint tkey = 0x66666666;
        IPAddress gameServerIP = IPAddress.Parse ("127.0.0.1");
        byte[] ipAsBytes = gameServerIP.GetAddressBytes ();
        const ushort gameServerPort = 4343;
        Message universeSelectedAck = null;
        using (MessageWriter writer = new MessageWriter (LoginMessageId.L2C_UNIVERSE_SELECTED_ACK))
        {
          writer.Write (statusCode);
          writer.Write (unknownDword);
          writer.Write (universePackageName);
          writer.Write (tkey);
          writer.Write (ipAsBytes[0]);
          writer.Write (ipAsBytes[1]);
          writer.Write (ipAsBytes[2]);
          writer.Write (ipAsBytes[3]);
          writer.Write (gameServerPort);

          universeSelectedAck = writer.ComputeMessage ();
        }
        universeSelected.clientSocket.Send (universeSelectedAck.data);
      }

    }

    enum eLoginRequestResult
    {
      LRR_NONE,
      LRR_INVALID_REVISION,
      LRR_INVALID_USERNAME,
      LRR_INVALID_PASSWORD,
      LRR_AUTHENTICATE_FAILED,
      LRR_LOGIN_ADD_FAILED,
      LRR_LOGIN_CONNECT_FAILED,
      LRR_INVALID_ACTIVE_CODE,
      LRR_BANNED_ACCOUNT,
      LRR_SUSPENDED_ACCOUNT,
      LRR_DISABLED_ACCOUNT
    }
  }
}
