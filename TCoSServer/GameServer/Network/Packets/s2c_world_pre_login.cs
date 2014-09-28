using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_world_pre_login : SBPacket
  {
    public uint Unknwown;//Must be 0
    public int WorldId;

    public override Message Generate ()
    {
      Message message = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_WORLD_PRE_LOGIN))
      {
        writer.Write (Unknwown);
        writer.Write (WorldId);
        message = writer.ComputeMessage ();
      }
      return message;
    }
  }
}
