using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using TCoSServer.Common;
using TCoSServer.GameServer.Network.Structures;
using System.IO;
using TCoSServer.GameServer.Network.Packets;

namespace TCoSServer.GameServer.Network
{

  /// <summary>
  /// Represents a connection with a specific player.
  /// </summary>
  class NetworkPlayer
  {
    private Gameplay.Player player;
    private Dictionary<GameMessageIds, Common.HandleMessageCallback> messageHandlers;

    public NetworkPlayer (Socket clientSocket, uint transportKey)
    {
      player = findPlayerWithTransportKey (transportKey);

      messageHandlers = new Dictionary<GameMessageIds, Common.HandleMessageCallback> (50);
      messageHandlers.Add (GameMessageIds.DISCONNECT, HandleDisconnect);
      messageHandlers.Add (GameMessageIds.C2S_WORLD_LOGOUT, HandleWorldLogout);
      messageHandlers.Add (GameMessageIds.C2S_WORLD_PRE_LOGIN_ACK, HandleWorldPreLoginAck);
      messageHandlers.Add (GameMessageIds.C2S_CS_CREATE_CHARACTER, HandleCSCreateCharacter);
      messageHandlers.Add (GameMessageIds.C2S_CS_SELECT_CHARACTER, HandleCSSelectCharacter);

      Message nextMessage = new Message();
      nextMessage.clientSocket = clientSocket;

      //Send world prelogin packet
      SendWorldPrelogin (clientSocket);

      try
      {
      //Start handling players messages
      clientSocket.BeginReceive (nextMessage.header, 0, Message.headerSize, 
                                  SocketFlags.None, HandleNewMessage, nextMessage);
      }
      catch (ObjectDisposedException)
      { }
    }

    private Gameplay.Player findPlayerWithTransportKey (uint transportKey)
    {
      //TODO with DB

      return new Gameplay.Player ();
    }

    private void HandleNewMessage (IAsyncResult ar)
    {
      Message message = (Message)ar.AsyncState;

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
        messageHandlers[(GameMessageIds)message.id] (message);
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
      Console.WriteLine ("[GS] Listen for next incoming message");
      try
      {
        handler.BeginReceive (nextMessage.header, 0, Message.headerSize, SocketFlags.None, HandleNewMessage, nextMessage);
      }
      catch (ObjectDisposedException)
      {  }
    }

    private void HandleWorldPreLoginAck (Message message)
    {
      message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
      uint statusCode;
      using (MessageReader reader = new MessageReader (message))
      {
        statusCode = reader.ReadUInt32 ();
      }
      Console.WriteLine ("[GS] Received C2S_WORLD_PRE_LOGIN_ACK with status {0}", statusCode);

      SendCSLogin (message.clientSocket);
    }

    private void HandleCSCreateCharacter (Message message)
    {
      Console.WriteLine ("[GS] Handle CS_CREATE_CHARACTER");
      message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
      c2s_cs_create_character createChar = new c2s_cs_create_character ();
      using (MessageReader reader = new MessageReader (message))
      {
        createChar.ReadFrom (reader);
      }
      SendCSCreateCharacterAck (createChar, message.clientSocket);
    }

    private void HandleCSSelectCharacter (Message message)
    {
    }

    private void HandleDisconnect (Message message)
    {
      Console.WriteLine ("Player disconnected");
      message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
      message.clientSocket.Close ();
    }

    private void HandleWorldLogout (Message message)
    {
      message.clientSocket.Receive (message.data, message.size, SocketFlags.None);
      Console.WriteLine ("Player send World Logout");
      message.clientSocket.Close ();
    }

    //Send messages
    private void SendWorldPrelogin (Socket clientSocket)
    {
      Message worldPreLogin = null;
      const uint statusCode = 0;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_WORLD_PRE_LOGIN, 8))
      {
        writer.Write (statusCode);
        writer.Write (Gameplay.World.CHARACTER_SELECTION_ID);
        worldPreLogin = writer.ComputeMessage ();
      }

      clientSocket.Send (worldPreLogin.data);
    }

    private void SendCSLogin (Socket clientSocket)
    {
      s2c_cs_login message = new s2c_cs_login ();
      //Character creation screen
      if (player.getNumCharacters () == 0)
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

    private void SendCSCreateCharacterAck (c2s_cs_create_character characterData, Socket clientSocket)
    {
      s2c_cs_create_character_ack ack = new s2c_cs_create_character_ack (characterData);
      Message message = ack.Generate ();
      clientSocket.Send (message.data);
    }
  }
}
