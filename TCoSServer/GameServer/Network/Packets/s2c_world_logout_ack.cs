using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_world_logout_ack : SBPacket
  {
    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_WORLD_LOGOUT_ACK))
      {
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
