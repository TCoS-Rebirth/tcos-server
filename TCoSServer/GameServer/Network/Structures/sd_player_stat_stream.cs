using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_player_stat_stream : SBStruct
  {
    public float FamePoints;
    public float PepPoints;
    public int Unknown1;
    public byte RemainingAttributePoints;
    public float CurrentHealth;
    public byte CameraMode;
    public int MoveSpeed;
    public sd_character_stats_record CharacterStats;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (FamePoints);
      writer.Write (PepPoints);
      writer.Write (Unknown1);
      writer.Write (RemainingAttributePoints);
      writer.Write (CurrentHealth);
      writer.Write (CameraMode);
      writer.Write (MoveSpeed);
      writer.Write (CharacterStats);
    }
  }
}
