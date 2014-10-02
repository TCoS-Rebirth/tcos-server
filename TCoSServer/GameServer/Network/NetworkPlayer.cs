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
using TCoSServer.GameServer.Gameplay;
using System.Collections;
using TCoSServer.GameServer.Gameplay.Enums;

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
      messageHandlers.Add (GameMessageIds.C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENT, HandleUpdateMovement);
      messageHandlers.Add (GameMessageIds.C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENTWITHPHYSICS, HandleUpdateMovementWithPhysics);
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
      //Dirty temporary cheat
      if (GameServer.BypassCharacterScreen)
        SendWorldPrelogin (clientSocket, World.PT_HAWKSMOUTH_ID);
      else
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
      try
      {
        int bytesRead = handler.EndReceive (ar);
      }
      catch (SocketException)
      { }

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
        try
        {
          handler.Receive (message.data, message.size, SocketFlags.None);//Extract the data but do nothing
        }
        catch (SocketException)
        { }
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

    private static bool temporaryBugfix = false;//Will then be fixed with gameplay layer
    private static void HandleWorldPreLoginAck (NetworkPlayer player, Message message)
    {
      c2s_world_pre_login_ack preLoginAck = new c2s_world_pre_login_ack ();
      preLoginAck.ReadFrom (message);
      Console.WriteLine ("[GS] Received C2S_WORLD_PRE_LOGIN_ACK with status {0}", preLoginAck.StatusCode);

      if (GameServer.BypassCharacterScreen)
      {
        Console.WriteLine ("Connecting direct to world");
        SendWorldLogin (message.clientSocket);
      }
      else if (!temporaryBugfix)
      {
        SendCSLogin (player, message.clientSocket);
        temporaryBugfix = true;
      }
      else
        SendWorldLogin (message.clientSocket);
    }

    //DEBUG REMOVE ME
    private static byte[] Lod0;
    private static byte[] Lod1;
    private static byte[] Lod2;
    private static byte[] Lod3;
    private static void HandleCSCreateCharacter (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle CS_CREATE_CHARACTER");
      c2s_cs_create_character createChar = new c2s_cs_create_character ();
      createChar.ReadFrom (message);
      Lod0 = createChar.Lod0;
      Lod1 = createChar.Lod1;
      Lod2 = createChar.Lod2;
      Lod3 = createChar.Lod3;
      Array.Reverse (Lod0);
      Array.Reverse (Lod1);
      Array.Reverse (Lod2);
      Array.Reverse (Lod3);

      SendCSCreateCharacterAck (createChar, message.clientSocket);
    }

    private static void HandleCSSelectCharacter (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle CS_SELECT_CHARACTER");
      SendWorldPrelogin (message.clientSocket, World.PT_HAWKSMOUTH_ID);
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
      Console.WriteLine ("[GS] Player send World Logout");
      SendWorldLogoutAck (message.clientSocket);
      message.clientSocket.Close ();
    }

    private static void HandleUpdateMovement (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENT");
      c2s_game_playerpawn_cl2sv_updatemovement packet = new c2s_game_playerpawn_cl2sv_updatemovement ();
      packet.ReadFrom (message);
      Console.WriteLine ("[GS] Unknown dword: {0}", packet.Unknown);
      Console.WriteLine ("[GS] Position: {0} {1} {2}", packet.Position.X, packet.Position.Y, packet.Position.Z);
      Console.WriteLine ("[GS] Direction: {0} {1} {2}", packet.Direction.X, packet.Direction.Y, packet.Direction.Z);
      Console.WriteLine ("[GS] FrameID: {0}", packet.MoveFrameID);

      //Test 
      s2r_game_playerpawn_move playerMove = new s2r_game_playerpawn_move ();
      playerMove.NetLocation = packet.Position;
      playerMove.NetVelocity = packet.Direction;
      playerMove.RelevanceID = 56;
      playerMove.MoveFrameID = packet.MoveFrameID;
      playerMove.Physics = (byte) EPhysics.PHYS_Walking;
      
      Message testMove = playerMove.Generate ();
      message.clientSocket.Send (testMove.data);
    }

    private static void HandleUpdateMovementWithPhysics (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENTWHITHPHYSICS");
      c2s_game_playerpawn_updatemovementwithpysics packet = new c2s_game_playerpawn_updatemovementwithpysics ();
      packet.ReadFrom (message);
      Console.WriteLine ("[GS] Unknown dword: {0}", packet.Unknown);
      Console.WriteLine ("[GS] Position: {0} {1} {2}", packet.Position.X, packet.Position.Y, packet.Position.Z);
      Console.WriteLine ("[GS] Direction: {0} {1} {2}", packet.Direction.X, packet.Direction.Y, packet.Direction.Z);
      Console.WriteLine ("[GS] Physics: {0}", (EPhysics)packet.Physics);
      Console.WriteLine ("[GS] FrameID: {0}", packet.FrameId);

      //Temp test
      if (packet.FrameId == 1)
      {
        s2c_player_add addPlayer = new s2c_player_add ();
        addPlayer.RelevanceID = 56;
        addPlayer.PlayerPawn.Physics = (byte) EPhysics.PHYS_Walking;
        addPlayer.PlayerPawn.PawnState = 1;
        addPlayer.FameLevel = 10;
        addPlayer.Concentration = 10;
        addPlayer.MaxHealth = 100;
        addPlayer.Morale = 5;
        addPlayer.PepRank = 2;
        addPlayer.Physique = 10;
        addPlayer.PlayerAppearance = new s2r_game_playerappearance_add_stream ();
        addPlayer.PlayerAppearance.Lod0 = Lod0;
        addPlayer.PlayerAppearance.Lod1 = Lod1;
        addPlayer.PlayerAppearance.Lod2 = Lod2;
        addPlayer.PlayerAppearance.Lod3 = Lod3;
        addPlayer.PlayerCharacter = new s2r_game_playercharacter_stream ();
        addPlayer.PlayerCharacter.Name = "Evhien";
        addPlayer.PlayerCharacter.FactionID = 1;
        addPlayer.PlayerCombatState = new s2r_game_combatstate_stream ();
        addPlayer.PlayerPawn = new s2r_game_playerpawn_add_stream ();
        addPlayer.PlayerPawn.MoveFrameID = 0;
        addPlayer.PlayerPawn.NetLocation = new FVector (500, 0, 6106.0f);
        addPlayer.PlayerPawn.NetVelocity = new FVector (0, 0, 0);
        addPlayer.PlayerPawn.Physics = 1;
        addPlayer.PlayerStats = new s2r_game_stats_add_stream ();
        addPlayer.PlayerStats.Health = 50;
        addPlayer.PlayerStats.MovementSpeed = 100;
        Message testMessage = addPlayer.Generate ();
        message.clientSocket.Send (testMessage.data);
      }
    }

    //Send messages
    private static void SendWorldPrelogin (Socket clientSocket, int worldId = 1)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_PRE_LOGIN");
      s2c_world_pre_login preLogin = new s2c_world_pre_login ();
      preLogin.Unknwown = 0;
      preLogin.WorldId = worldId;
      Message message = preLogin.Generate ();
      clientSocket.Send (message.data);
    }

    private static void SendWorldLogoutAck (Socket clientSocket)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_LOGOUT_ACK");
      s2c_world_logout_ack logout = new s2c_world_logout_ack ();
      Message message = logout.Generate ();
      clientSocket.Send (message.data);
    }

    private static void SendCSLogin (NetworkPlayer nPlayer, Socket clientSocket)
    {
      Console.WriteLine ("[GS] Send S2C_CS_LOGIN");
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
      Console.WriteLine ("[GS] Send S2C_CS_CREATE_CHARACTER_ACK");
      s2c_cs_create_character_ack ack = new s2c_cs_create_character_ack (characterData);
      Message message = ack.Generate ();
      clientSocket.Send (message.data);
    }

    private static void SendWorldLogin (Socket clientSocket)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_LOGIN");
      s2c_world_login worldLogin = new s2c_world_login ();
      worldLogin.Unknown1 = 0;
      worldLogin.ActorId = 1;
      worldLogin.PawnStream = new sd_playerpawn_login_stream ();
      worldLogin.PawnStream.BaseMoveSpeed = 2;
      worldLogin.PawnStream.PhysicType = 1;
      worldLogin.PawnStream.PawnState = 1;
      worldLogin.PawnStream.MoveFrameID = 1;

      worldLogin.PlayerStatsStream = new sd_player_stat_stream ();
      worldLogin.PlayerStatsStream.MoveSpeed = 100;
      worldLogin.PlayerStatsStream.CurrentHealth = 100.0f;
      worldLogin.PlayerStatsStream.CharacterStats = new sd_character_stats_record ();

      worldLogin.CharacterInfo = new sd_base_character_info ();
      worldLogin.CharacterInfo.CharacterId = 42;
      worldLogin.CharacterInfo.CharacterData = new sd_character_data ();
      worldLogin.CharacterInfo.CharacterSheetData = new sd_character_sheet_data ();
      worldLogin.CharacterInfo.CharacterSheetData.ClassId = 2;
      worldLogin.CharacterInfo.CharacterSheetData.Health = 100.0f;
      worldLogin.CharacterInfo.CharacterSheetData.SelectedSkillDeckID = 0;

      worldLogin.CharacterInfo.CharacterData.Name = "Tykaru";
      worldLogin.CharacterInfo.CharacterData.Position = new FVector (0, 0, 6200);
      worldLogin.CharacterInfo.CharacterData.WorldId = World.PT_HAWKSMOUTH_ID;
      worldLogin.CharacterInfo.CharacterData.Dead = 0;
      worldLogin.CharacterInfo.CharacterData.AppearancePart1 = 1;

      worldLogin.UnknownSlider = 1;
      worldLogin.PlayerGroup = 0;

      Message message = worldLogin.Generate ();
      clientSocket.Send (message.data);
    }
  }
}
