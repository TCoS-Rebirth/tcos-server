using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_travel_connect : SBPacket
  {
    public int TransportKey;

    protected override void InternalRead (MessageReader reader)
    {
      TransportKey = reader.ReadInt32 (); 
    }
  }
}
