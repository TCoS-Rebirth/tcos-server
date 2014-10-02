using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  // WARNING!! Same for Player, NPC and Unarmed Combat states streams, and same for ADD and LOGIN streams
  struct s2r_game_combatstate_stream : SBStruct
  {
    public byte CombatMode;
    public int MainWeapon;
    public int OffhandWeapon;
    
    public void WriteTo (MessageWriter writer)
    {
      writer.Write (CombatMode);
      writer.Write (MainWeapon);
      writer.Write (OffhandWeapon);
    }
  }
}
