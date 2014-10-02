using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  // WARNING: Same struct for LOGIN stream and ADD stream
  struct s2r_game_playercharacter_stream : SBStruct
  {
    public string Name;
    public string GuildName;
    public int FactionID;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Name);
      writer.Write (GuildName);
      writer.Write (FactionID);
    }
  }
}
