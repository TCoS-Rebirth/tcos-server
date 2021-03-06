﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_cs_login : SBPacket
  {
    public sd_base_character_info[] BaseCharacterInfo;
    public Dictionary<int, int> FameMap;

    public override Message Generate ()
    {
      Message result;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_CS_LOGIN))
      {
        writer.Write (BaseCharacterInfo);

        if (FameMap == null)
          writer.Write (0);
        else
        {
          writer.Write (FameMap.Count);
          foreach (KeyValuePair<int, int> keyValue in FameMap)
          {
            writer.Write (keyValue.Key);
            writer.Write (keyValue.Value);
          }
        }
        result = writer.ComputeMessage ();
      }

      return result;
    }
  }
}
