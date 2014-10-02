using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  // WARNING: It's the same struct for NCPStats, PlayerStats and CharacterStats
  struct s2r_game_stats_add_stream : SBStruct
  {
    public float Health;
    public byte FrozenFlags;
    public int MovementSpeed;
    public int StateRankShift;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Health);
      writer.Write (FrozenFlags);
      writer.Write (MovementSpeed);
      writer.Write (StateRankShift);
    }
  }
}
