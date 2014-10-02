using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_skill : SBStruct
  {
    public int SkillID;
    public byte NumSigilSlots;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (SkillID);
      writer.Write (NumSigilSlots);
    }
  }
}
