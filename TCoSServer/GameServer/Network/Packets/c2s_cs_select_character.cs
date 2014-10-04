using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_cs_select_character : SBPacket
  {
    public int CharacterID;

    protected override void InternalRead (MessageReader reader)
    {
      CharacterID = reader.ReadInt32 ();
    }

  }
}
