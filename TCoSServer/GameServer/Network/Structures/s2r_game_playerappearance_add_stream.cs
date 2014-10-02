using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct s2r_game_playerappearance_add_stream : SBStruct
  {
    public byte[] Lod0;
    public byte[] Lod1;
    public byte[] Lod2;
    public byte[] Lod3;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Lod0);
      writer.Write (Lod1);
      writer.Write (Lod2);
      writer.Write (Lod3);
    }
  }
}
