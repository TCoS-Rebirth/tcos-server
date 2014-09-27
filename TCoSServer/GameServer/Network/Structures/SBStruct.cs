using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  public interface SBStruct
  {
    void WriteTo (MessageWriter writer);
  }
}
