using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_player_add : SBPacket
  {
    public int Unknown1;
    public int Unknown2;
    public int MaxHealth;
    public float Physique;
    public float Morale;
    public float Concentration;
    public int FameLevel;
    public int PepRank;
    public s2r_game_playerpawn_add_stream PlayerPawn;
    public int ShiftableAppearance;
    public s2r_game_playerappearance_add_stream PlayerAppearance;
    public s2r_game_playercharacter_stream PlayerCharacter;
    public s2r_game_stats_add_stream PlayerStats;
    public s2r_game_combatstate_stream PlayerCombatState;
    public s2r_game_effects_stream Effects;

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_PLAYER_ADD))
      {
        writer.Write (Unknown1);
        writer.Write (Unknown2);
        writer.Write (MaxHealth);
        writer.Write (Physique);
        writer.Write (Morale);
        writer.Write (Concentration);
        writer.Write (FameLevel);
        writer.Write (PepRank);
        writer.Write (PlayerPawn);
        writer.Write (ShiftableAppearance);
        writer.Write (PlayerAppearance);
        writer.Write (PlayerCharacter);
        writer.Write (PlayerStats);
        writer.Write (PlayerCombatState);
        writer.Write (Effects);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
