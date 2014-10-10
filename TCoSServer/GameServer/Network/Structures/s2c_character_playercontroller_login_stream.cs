using System;
using System.Collections.Generic;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  class s2c_character_playercontroller_login_stream : SBStruct
  {
    public float ServerTime;
    public byte NetState;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (ServerTime);
      writer.Write (NetState);
    }

  }
}
