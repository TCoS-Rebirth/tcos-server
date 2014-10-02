using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_quest : SBStruct
  {
    public int Unknown1;
    public int Unkwown2;
    public int QuestObjective;
    public int Unkwnown3;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Unknown1);
      writer.Write (Unkwown2);
      writer.Write (QuestObjective);
      writer.Write (Unkwnown3);
    }
  }
}
