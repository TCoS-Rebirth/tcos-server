using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_world_pre_login_ack : SBPacket
  {
    public int StatusCode;//seems to always been equals to 0 because no reason to not ack

    protected override void InternalRead (MessageReader reader)
    {
      StatusCode = reader.ReadInt32 ();
    }
  }
}
