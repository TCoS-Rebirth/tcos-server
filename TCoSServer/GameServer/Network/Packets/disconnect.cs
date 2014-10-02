using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class disconnect : SBPacket
  {
    public int Status;
    public string Reason;

    protected override void InternalRead (MessageReader reader)
    {
      Status = reader.ReadInt32 ();
      Reason = reader.ReadString (); 
    }
  }
}
