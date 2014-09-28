using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using TCoSServer.Common;
using TCoSServer.GameServer.Network.Structures;
using System.IO;
using TCoSServer.GameServer.Network.Packets;
using System.Diagnostics;

namespace TCoSServer.GameServer.Network
{

  /// <summary>
  /// Represents a connection with a specific player.
  /// Handle player related messages.
  /// </summary>
  class NetworkPlayer
  {
    private struct NetworkPlayerState
    {
      public NetworkPlayer player;
      public Message message;
    }

    private Gameplay.Player player;
    private delegate void HandleMessageCallback (NetworkPlayer player, Message message);

    private static Dictionary<GameMessageIds, HandleMessageCallback> messageHandlers;

    public static void InitMessageHandlers ()
    {
      messageHandlers = new Dictionary<GameMessageIds, HandleMessageCallback> (50);
      messageHandlers.Add (GameMessageIds.DISCONNECT, HandleDisconnect);
      messageHandlers.Add (GameMessageIds.C2S_WORLD_LOGOUT, HandleWorldLogout);
      messageHandlers.Add (GameMessageIds.C2S_WORLD_PRE_LOGIN_ACK, HandleWorldPreLoginAck);
      messageHandlers.Add (GameMessageIds.C2S_CS_CREATE_CHARACTER, HandleCSCreateCharacter);
      messageHandlers.Add (GameMessageIds.C2S_CS_SELECT_CHARACTER, HandleCSSelectCharacter);
    }

    public NetworkPlayer (Socket clientSocket, uint transportKey)
    {
      player = findPlayerWithTransportKey (transportKey);

      Message nextMessage = new Message();
      nextMessage.clientSocket = clientSocket;

      NetworkPlayerState state = new NetworkPlayerState ();
      state.message = nextMessage;
      state.player = this;

      //Send world prelogin packet
      SendWorldPrelogin (clientSocket);

      try
      {
      //Start handling players messages
      clientSocket.BeginReceive (nextMessage.header, 0, Message.headerSize, 
                                  SocketFlags.None, HandleNewMessage, state);
      }
      catch (ObjectDisposedException)
      { }
    }

    private Gameplay.Player findPlayerWithTransportKey (uint transportKey)
    {
      //TODO with DB

      return new Gameplay.Player ();
    }

    /// <summary>
    /// Asynchronous message handling happens here.
    /// The next message header is parsed and according to the message
    /// id, the correct message handler method  is called.
    /// </summary>
    /// <param name="ar">Result of the asynchronous operation</param>
    private static void HandleNewMessage (IAsyncResult ar)
    {
      NetworkPlayerState state = (NetworkPlayerState)ar.AsyncState;
      Message message = state.message;

      Socket handler = message.clientSocket;
      if (handler == null)
      {
        Console.WriteLine ("[GS] WARNING: NULL SOCKET");
        return;
      }
      int bytesRead = handler.EndReceive (ar);

      using (MessageReader reader = new MessageReader (message))
      {
        //Analyse header (automatically done by MessageReader)
      }
      Console.WriteLine ("[GS] Received new message {0} of size {1}", message.id, message.size);
      //Handle message
      try
      {
        messageHandlers[(GameMessageIds)message.id] (state.player, message);
      }
      catch (KeyNotFoundException)
      {
        Console.WriteLine ("[GS] Packet number {0} not implemented yet", message.id);
        handler.Receive (message.data, message.size, SocketFlags.None);//Extract the data but do nothing
        if (message.id == 0)//Special case
          return;
      }

      //If last message was a disconnect, stop listening
      if (!handler.Connected)
        return;

      //Listen for new message
      Message nextMessage = new Message ();
      nextMessage.clientSocket = handler;
      state.message = nextMessage;
      Console.WriteLine ("[GS] Listen for next incoming message");
      try
      {
        handler.BeginReceive (nextMessage.header, 0, Message.headerSize, SocketFlags.None, HandleNewMessage, state);
      }
      catch (ObjectDisposedException)
      {  }
    }

    private static void HandleWorldPreLoginAck (NetworkPlayer player, Message message)
    {
      c2s_world_pre_login_ack preLoginAck = new c2s_world_pre_login_ack ();
      preLoginAck.ReadFrom (message);
      Console.WriteLine ("[GS] Received C2S_WORLD_PRE_LOGIN_ACK with status {0}", preLoginAck.StatusCode);

      if (GameServer.BypassCharacterScreen)
      {
        //TODO
      }

      SendCSLogin (player, message.clientSocket);
    }

    private static void HandleCSCreateCharacter (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle CS_CREATE_CHARACTER");
      c2s_cs_create_character createChar = new c2s_cs_create_character ();
      createChar.ReadFrom (message);
      
      SendCSCreateCharacterAck (createChar, message.clientSocket);
    }

    private static void HandleCSSelectCharacter (NetworkPlayer player, Message message)
    {
    }

    private static void HandleDisconnect (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Player disconnected");
      disconnect dis = new disconnect ();
      dis.ReadFrom (message);
      Console.WriteLine ("[GS] Reason: {0}. Status: {1}", dis.Reason, dis.Status);
      message.clientSocket.Close ();
    }

    private static void HandleWorldLogout (NetworkPlayer player, Message message)
    {
      c2s_world_logout packet = new c2s_world_logout ();
      packet.ReadFrom (message);
      Console.WriteLine ("[GS] Player send World Logout");
      message.clientSocket.Close ();
    }

    //Send messages
    private static void SendWorldPrelogin (Socket clientSocket)
    {
      s2c_world_pre_login preLogin = new s2c_world_pre_login ();
      preLogin.Unknwown = 0;
      preLogin.WorldId = 1;
      Message message = preLogin.Generate ();
      clientSocket.Send (message.data);
    }

    private static void SendCSLogin (NetworkPlayer nPlayer, Socket clientSocket)
    {
      s2c_cs_login message = new s2c_cs_login ();
      //Character creation screen
      if (nPlayer.player.getNumCharacters () == 0)
      {
        Message csLogin = message.Generate ();

        clientSocket.Send (csLogin.data);
      }
      //Character selection screen
      else
      {
        sd_base_character_info baseInfo = new sd_base_character_info ();
        baseInfo.CharacterId = 42;

        sd_character_data charData = new sd_character_data ();
        charData.AccountId = 1;
        charData.AppearancePart1 = 1;
        charData.Name = "Mertyuiolpmoiuyt";

        baseInfo.CharacterData = charData;

        sd_character_sheet_data characterSheet = new sd_character_sheet_data ();
        baseInfo.CharacterSheetData = characterSheet;
        message.BaseCharacterInfo = new sd_base_character_info[1];
        message.BaseCharacterInfo[0] = baseInfo;

        message.FameMap = new Dictionary<int, int> ();
        message.FameMap.Add (42, 5);

        Message csLogin = message.Generate ();
        clientSocket.Send (csLogin.data);
      }
    }

    private static void SendCSCreateCharacterAck (c2s_cs_create_character characterData, Socket clientSocket)
    {
      s2c_cs_create_character_ack ack = new s2c_cs_create_character_ack (characterData);
      Message message = ack.Generate ();
      clientSocket.Send (message.data);
    }
  }
}
