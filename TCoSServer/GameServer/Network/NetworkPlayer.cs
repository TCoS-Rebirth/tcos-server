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
    private List<NetworkPlayer> replicationList;
    private Socket ConnectionToClient;
    private delegate void HandleMessageCallback (NetworkPlayer player, Message message);
    private Connection connection;
    private static int nextRelevanceId = 0;

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
      messageHandlers.Add (GameMessageIds.C2S_GAME_PLAYERPAWN_CL2SV_UPDATEROTATION, HandleUpdateRotation);
      messageHandlers.Add (GameMessageIds.C2S_GAME_CHAT_CL2SV_SENDMESSAGE, HandleChatMessage);
      messageHandlers.Add (GameMessageIds.C2S_INTERACTIVELEVELELEMENT_CL2SV_ONRADIALMENUOPTION, HandleILEOnRadialMenuOption);
    }

    public NetworkPlayer (Socket clientSocket, uint transportKey, ref List<NetworkPlayer> replicationList)
    {
      player = findPlayerWithTransportKey (transportKey);
      ConnectionToClient = clientSocket;
      connection = new Connection (clientSocket);

      Message nextMessage = new Message();
      nextMessage.clientSocket = clientSocket;

      NetworkPlayerState state = new NetworkPlayerState ();
      state.message = nextMessage;
      state.player = this;
      this.replicationList = replicationList;

      //Send world prelogin packet
      //Dirty temporary cheat
      if (GameServer.BypassCharacterScreen)
        SendWorldPrelogin (connection, World.PT_HAWKSMOUTH_ID);
      else
        SendWorldPrelogin (connection);

      try
      {
      //Start handling players messages
      clientSocket.BeginReceive (nextMessage.header, 0, Message.headerSize, 
                                  SocketFlags.None, HandleNewMessage, state);
      }
      catch (ObjectDisposedException)
      { }
    }

    public void SendMessage (Message message)
    {
      connection.Send (message);
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

    //Handle sending all the replication messages to other players
    private void NotifyReplication (Message toSend)
    {
      foreach (NetworkPlayer player in replicationList)
      {
        if (player.player.AccountID == this.player.AccountID)
          continue;
        player.connection.Send (toSend);
      }
    }

    private static void HandleWorldPreLoginAck (NetworkPlayer player, Message message)
    {
      c2s_world_pre_login_ack preLoginAck = new c2s_world_pre_login_ack ();
      preLoginAck.ReadFrom (message);
      Console.WriteLine ("[GS] Received C2S_WORLD_PRE_LOGIN_ACK with status {0}", preLoginAck.StatusCode);

      if (GameServer.BypassCharacterScreen)
      {
        //There is no real gameplay layer currently so for now it's "fake code" holding properties
        player.player.SetCurrentCharacterById (nextRelevanceId++);
        player.player.CurrentCharacter.CurrentWorldID = World.PT_HAWKSMOUTH_ID;
        Console.WriteLine ("[GS]Connecting direct to world...");
        Console.WriteLine("[GS]Character relevance id: {0}", nextRelevanceId-1);
        SendWorldLogin (player);
      }
      else if (player.player.CurrentCharacter == null)
      {
        SendCSLogin (player);
      }
      else
        SendWorldLogin (player);
    }

    //DEBUG REMOVE ME
    private static byte[] Lod0;
    private static byte[] Lod1;
    private static byte[] Lod2;
    private static byte[] Lod3;
    private static FVector pawnLocation = new FVector (500, 0, 6106.0f);
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

      Character character = new Character (nextRelevanceId++);
      character.Name = createChar.Name;
      Console.WriteLine("[GS] New character with id: {0} and name: {1} and relevance id: {2}", character.ID, character.Name, nextRelevanceId-1);
      player.player.AddNewCharacter (character);
      player.player.SetCurrentCharacterById (character.ID);
      SendCSCreateCharacterAck (createChar, player);
    }

    private static void HandleCSSelectCharacter (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle CS_SELECT_CHARACTER");
      c2s_cs_select_character select = new c2s_cs_select_character();
      select.ReadFrom (message);
      player.player.SetCurrentCharacterById (select.CharacterID);
      player.player.CurrentCharacter.CurrentWorldID = World.PT_HAWKSMOUTH_ID;
      Console.WriteLine ("[GS] Player connect with character: [{0}] {1}", player.player.CurrentCharacter.ID, player.player.CurrentCharacter.Name);
      SendWorldPrelogin (player.connection, player.player.CurrentCharacter.CurrentWorldID);
    }

    private static void HandleDisconnect (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Player disconnected");
      disconnect dis = new disconnect ();
      dis.ReadFrom (message);
      Console.WriteLine ("[GS] Reason: {0}. Status: {1}", dis.Reason, dis.Status);
      player.connection.Close ();
    }

    private static void HandleWorldLogout (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Player send World Logout");
      if (player.player.CurrentCharacter != null)
       player.player.CurrentCharacter.CurrentWorldID = World.CHARACTER_SELECTION_ID;
      SendWorldLogoutAck (player.connection);
    }

    private static void HandleUpdateMovement (NetworkPlayer player, Message message)
    {
      //Console.WriteLine ("[GS] Handle C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENT");
      c2s_game_playerpawn_cl2sv_updatemovement packet = new c2s_game_playerpawn_cl2sv_updatemovement ();
      packet.ReadFrom (message);
      player.player.CurrentCharacter.Position = packet.Position;

      //Test 
      s2r_game_playerpawn_move playermove = new s2r_game_playerpawn_move ();
      playermove.NetLocation = packet.Position;
      playermove.NetVelocity = packet.Velocity;
      playermove.RelevanceID = player.player.CurrentCharacter.ID; ;
      playermove.MoveFrameID = packet.MoveFrameID;
      playermove.Physics = (byte) EPhysics.PHYS_Walking;
      player.NotifyReplication (playermove.Generate ());
    }

    private static void HandleUpdateMovementWithPhysics (NetworkPlayer player, Message message)
    {
      //Console.WriteLine ("[GS] Handle C2S_GAME_PLAYERPAWN_CL2SV_UPDATEMOVEMENTWITHPHYSICS");
      c2s_game_playerpawn_updatemovementwithpysics packet = new c2s_game_playerpawn_updatemovementwithpysics ();
      packet.ReadFrom (message);
      player.player.CurrentCharacter.Position = packet.Position;

      //Test 
      s2r_game_playerpawn_move playermove = new s2r_game_playerpawn_move ();
      playermove.NetLocation = packet.Position;
      playermove.NetVelocity = packet.Velocity;
      playermove.RelevanceID = player.player.CurrentCharacter.ID;
      playermove.MoveFrameID = packet.MoveFrameID;
      playermove.Physics = packet.Physics;
      player.NotifyReplication (playermove.Generate ());
    }

    private static void HandleUpdateRotation (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle C2S_GAME_PLAYERPAWN_CL2SV_UPDATEROTATION");
      c2s_game_playerpawn_cl2sv_updaterotation rotation = new c2s_game_playerpawn_cl2sv_updaterotation ();
      rotation.ReadFrom (message);

      s2r_game_playerpawn_updaterotation notifyRotation = new s2r_game_playerpawn_updaterotation ();
      notifyRotation.RelevanceID = rotation.CharacterID;
      notifyRotation.CompressedRotator = rotation.CompressedRotator;
      player.NotifyReplication (notifyRotation.Generate ());
    }

    private static void HandleChatMessage (NetworkPlayer player, Message message)
    {
      Console.WriteLine ("[GS] Handle C2S_GAME_CHAT_CL2SV_SENDMESSAGE");
      c2s_game_chat_cl2sv_sendmessage packet = new c2s_game_chat_cl2sv_sendmessage ();
      packet.ReadFrom (message);
      Console.WriteLine ("{0} sends {1} on {2} to {3}",
      packet.CharacterID, packet.Message, packet.Channel, packet.Receiver);

      s2c_game_chat_sv2cl_onmessage answer = new s2c_game_chat_sv2cl_onmessage ();
      answer.Sender = player.player.CurrentCharacter.Name;
      answer.Message = packet.Message;
      answer.Channel = packet.Channel;
      player.NotifyReplication (answer.Generate ());
    }

    //Send messages
    private static void SendWorldPrelogin (Connection connection, int worldId = 1)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_PRE_LOGIN");
      s2c_world_pre_login preLogin = new s2c_world_pre_login ();
      preLogin.Unknwown = 0;
      preLogin.WorldId = worldId;
      Message message = preLogin.Generate ();
      connection.Send (message);
    }

    private static void SendWorldLogoutAck (Connection connection)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_LOGOUT_ACK");
      s2c_world_logout_ack logout = new s2c_world_logout_ack ();
      Message message = logout.Generate ();
      connection.Send (message);
    }

    
    private static void SendCSLogin (NetworkPlayer nPlayer)
    {
      Console.WriteLine ("[GS] Send S2C_CS_LOGIN");
      s2c_cs_login message = new s2c_cs_login ();
      //Character creation screen
      if (nPlayer.player.getNumCharacters () == 0)
      {
        Message csLogin = message.Generate ();

        nPlayer.connection.Send (csLogin);
      }
      //Character selection screen
      else
      {
        int counter = 0;
        message.FameMap = new Dictionary<int, int> ();
        foreach (Character character in nPlayer.player.Characters)
        {
          sd_base_character_info baseInfo = new sd_base_character_info ();
          baseInfo.CharacterId = character.ID;

          sd_character_data charData = new sd_character_data ();
          charData.AccountId = nPlayer.player.AccountID;
          charData.Name = character.Name;

          baseInfo.CharacterData = charData;

          sd_character_sheet_data characterSheet = new sd_character_sheet_data ();
          baseInfo.CharacterSheetData = characterSheet;
          message.BaseCharacterInfo = new sd_base_character_info[1];
          message.BaseCharacterInfo[counter] = baseInfo;
          message.FameMap.Add (baseInfo.CharacterId, 5);
        }

        Message csLogin = message.Generate ();
        nPlayer.connection.Send (csLogin);
      }
    }

    private static void SendCSCreateCharacterAck (c2s_cs_create_character characterData, NetworkPlayer nPlayer)
    {
      Console.WriteLine ("[GS] Send S2C_CS_CREATE_CHARACTER_ACK");
      s2c_cs_create_character_ack ack = new s2c_cs_create_character_ack (characterData, nPlayer.player.CurrentCharacter.ID);
      ack.CharacterInformation.CharacterId = nPlayer.player.CurrentCharacter.ID;
      ack.CharacterInformation.CharacterData.AccountId = nPlayer.player.AccountID;
      Message message = ack.Generate ();
      nPlayer.connection.Send (message);
    }

    private static void SendWorldLogin (NetworkPlayer networkPlayer)
    {
      Console.WriteLine ("[GS] Send S2C_WORLD_LOGIN");
      networkPlayer.player.CurrentCharacter.Position = new FVector (-15102.36f, -7615.43f, 8004.88f);
      s2c_world_login worldLogin = new s2c_world_login ();
      worldLogin.ZeroDWord = 0;
      worldLogin.ActorId = networkPlayer.player.CurrentCharacter.ID;
      worldLogin.PlayerControllerStream = new s2c_character_playercontroller_login_stream ();
      worldLogin.PawnStream = new sd_playerpawn_login_stream ();
      worldLogin.PawnStream.BaseMoveSpeed = 2;
      worldLogin.PawnStream.PhysicType = 1;
      worldLogin.PawnStream.PawnState = (byte)EPhysics.PHYS_Walking;
      worldLogin.PawnStream.MoveFrameID = 1;

      worldLogin.PlayerStatsStream = new sd_player_stat_stream ();
      worldLogin.PlayerStatsStream.MoveSpeed = 1000;
      worldLogin.PlayerStatsStream.CurrentHealth = 500.0f;
      worldLogin.PlayerStatsStream.CharacterStats = new sd_character_stats_record ();

      worldLogin.CharacterInfo = new sd_base_character_info ();
      worldLogin.CharacterInfo.CharacterId = networkPlayer.player.CurrentCharacter.ID;
      worldLogin.CharacterInfo.CharacterData = new sd_character_data ();
      worldLogin.CharacterInfo.CharacterSheetData = new sd_character_sheet_data ();
      worldLogin.CharacterInfo.CharacterSheetData.ClassId = 2;
      worldLogin.CharacterInfo.CharacterSheetData.Health = 500.0f;
      worldLogin.CharacterInfo.CharacterSheetData.SelectedSkillDeckID = 0;

      worldLogin.CharacterInfo.CharacterData.Name = networkPlayer.player.CurrentCharacter.Name;
      worldLogin.CharacterInfo.CharacterData.Position = networkPlayer.player.CurrentCharacter.Position;
      worldLogin.CharacterInfo.CharacterData.WorldId = World.PT_HAWKSMOUTH_ID;
      worldLogin.CharacterInfo.CharacterData.Dead = 0;
      worldLogin.CharacterInfo.CharacterData.AppearancePart1 = 1;

      worldLogin.UnknownSlider = 1;
      worldLogin.PlayerGroup = 2;

      Message message = worldLogin.Generate ();
      networkPlayer.connection.Send (message);

      //Notify other players this player arrives
      s2c_player_add addPlayer = new s2c_player_add ();
      addPlayer.RelevanceID = networkPlayer.player.CurrentCharacter.ID;
      addPlayer.Unknown2 = 0;
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
      addPlayer.PlayerCharacter.Name = networkPlayer.player.CurrentCharacter.Name;
      addPlayer.PlayerCharacter.FactionID = 1;
      addPlayer.PlayerCombatState = new s2r_game_combatstate_stream ();
      addPlayer.PlayerPawn = new s2r_game_playerpawn_add_stream ();
      addPlayer.PlayerPawn.Physics = (byte)EPhysics.PHYS_Walking;
      addPlayer.PlayerPawn.PawnState = 1;
      addPlayer.PlayerPawn.DebugFilters = 0;
      addPlayer.PlayerPawn.GroundSpeedModifier = 100;
      addPlayer.PlayerPawn.MoveFrameID = 0;
      addPlayer.PlayerPawn.NetLocation = networkPlayer.player.CurrentCharacter.Position;
      addPlayer.PlayerPawn.NetVelocity = new FVector (0, 0, 0);
      addPlayer.PlayerPawn.Physics = (byte)EPhysics.PHYS_None;
      addPlayer.PlayerStats = new s2r_game_stats_add_stream ();
      addPlayer.PlayerStats.Health = 50;
      addPlayer.PlayerStats.MovementSpeed = 100;
      addPlayer.PlayerStats.FrozenFlags = 0;
      Message testMessage = addPlayer.Generate ();
      networkPlayer.NotifyReplication (testMessage);

      //Notify this player of other players
      foreach (NetworkPlayer player in networkPlayer.replicationList)
      {
        if (player.player.AccountID == networkPlayer.player.AccountID
          || player.player.CurrentCharacter == null /*character creation phase*/)
          continue;
        s2c_player_add addPlayerTwo = new s2c_player_add ();
        addPlayerTwo.RelevanceID = player.player.CurrentCharacter.ID;
        addPlayerTwo.Unknown2 = 5;
        addPlayerTwo.FameLevel = 10;
        addPlayerTwo.Concentration = 10;
        addPlayerTwo.MaxHealth = 100;
        addPlayerTwo.Morale = 5;
        addPlayerTwo.PepRank = 2;
        addPlayerTwo.Physique = 10;
        addPlayerTwo.PlayerAppearance = new s2r_game_playerappearance_add_stream ();
        addPlayerTwo.PlayerAppearance.Lod0 = Lod0;
        addPlayerTwo.PlayerAppearance.Lod1 = Lod1;
        addPlayerTwo.PlayerAppearance.Lod2 = Lod2;
        addPlayerTwo.PlayerAppearance.Lod3 = Lod3;
        addPlayerTwo.PlayerCharacter = new s2r_game_playercharacter_stream ();
        addPlayerTwo.PlayerCharacter.Name = player.player.CurrentCharacter.Name;
        addPlayerTwo.PlayerCharacter.FactionID = 1;
        addPlayerTwo.PlayerCombatState = new s2r_game_combatstate_stream ();
        addPlayerTwo.PlayerPawn = new s2r_game_playerpawn_add_stream ();
        addPlayerTwo.PlayerPawn.Physics = (byte)EPhysics.PHYS_Walking;
        addPlayerTwo.PlayerPawn.PawnState = 1;
        addPlayerTwo.PlayerPawn.MoveFrameID = 0;
        addPlayerTwo.PlayerPawn.NetLocation = player.player.CurrentCharacter.Position;
        addPlayerTwo.PlayerPawn.NetVelocity = new FVector (0, 0, 0);
        addPlayerTwo.PlayerStats = new s2r_game_stats_add_stream ();
        addPlayerTwo.PlayerStats.Health = 50;
        addPlayerTwo.PlayerStats.MovementSpeed = 500;
        addPlayerTwo.PlayerPawn.GroundSpeedModifier = 100;
        addPlayerTwo.PlayerStats.FrozenFlags = 0;
        Message testMessage2 = addPlayerTwo.Generate ();
        networkPlayer.connection.Send (testMessage2);
      }

       //Activate every relevance object (test)
       //Hardcoded values for PT_Hawksmouth map
      for (int i = 0; i < 79; ++i)
      {
        //Crashing ids (not InteractiveLevelElements but GameActors...)
        if (i == 12 || i == 62 || i == 65)
              continue;
        s2c_interactivelevelelement_add packet = new s2c_interactivelevelelement_add ();
        packet.RelevanceID = nextRelevanceId++;
        packet.NetRotation = new FVector ();
        packet.NetLocation = new FVector ();
        packet.LevelObjectID = i;
        packet.IsEnabledBitfield = 1;
        packet.IsHidden = 0;
        packet.CollisionType = 2;
        packet.ActivatedRespawnTimerBitfield = 0;
        Message interactiveAdd = packet.Generate ();
        networkPlayer.connection.Send (interactiveAdd);
      }
         
    }

    private static void HandleILEOnRadialMenuOption(NetworkPlayer nPlayer, Message message)
    {
        Console.WriteLine("[GS] Handle C2S_INTERACTIVELEVELELEMENT_CL2SV_ONRADIALMENUOPTION");
        c2s_interactivelevelelement_cl2sv_onradialmenuoption packet = new c2s_interactivelevelelement_cl2sv_onradialmenuoption();
        packet.ReadFrom(message);
        Console.WriteLine("1={0} ; 2={1} ; 3={2} ",
        packet.InteractiveLevelElementRelevanceId, packet.CharacterId, packet.RadialMenuOptions);

        //Works for mailboxes...
        Console.WriteLine("[GS] Send S2R_INTERACTIVELEVELELEMENT_SV2CLREL_STARTCLIENTSUBACTION");
        s2r_interactivelevelelement_sv2clrel_startclientsubaction answer = new s2r_interactivelevelelement_sv2clrel_startclientsubaction();
        answer.InteractiveLevelElementRelevanceId = packet.InteractiveLevelElementRelevanceId;
        answer.Unknown1 = 0;
        answer.RadialMenuOption = packet.RadialMenuOptions;
        answer.Unused1 = 0;
        answer.CompareFlag = 0;
        Message answerMessage = answer.Generate();
        nPlayer.connection.Send(answerMessage);

        Console.WriteLine("[GS] Send S2R_INTERACTIVELEVELELEMENT_SV2CLREL_ENDCLIENTSUBACTION");
        s2r_interactivelevelelement_sv2clrel_endclientsubaction answer2 = new s2r_interactivelevelelement_sv2clrel_endclientsubaction();
        answer2.InteractiveLevelElementRelevanceId = packet.InteractiveLevelElementRelevanceId;
        answer2.Unknown1 = 0;//If '1', mailboxes do not work anymore (maybe Action index)
        answer2.RadialMenuOption = packet.RadialMenuOptions;
        answer2.Unknown3 = 0;
        Message answer2Message = answer2.Generate();
        nPlayer.connection.Send(answer2Message);
    }
    
  }
}
