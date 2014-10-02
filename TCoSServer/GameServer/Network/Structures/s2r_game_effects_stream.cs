using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  // WARNING: It's the same struct for LOGIN and ADD stream
  struct s2r_game_effects_stream : SBStruct
  {
    public int[] Effects;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Effects);
    }
  }
}
