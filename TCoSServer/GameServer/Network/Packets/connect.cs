using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class connect : SBPacket
  {
    public int Status;

    protected override void InternalRead (MessageReader reader)
    {
      Status = reader.ReadInt32 ();
    }
  }
}
