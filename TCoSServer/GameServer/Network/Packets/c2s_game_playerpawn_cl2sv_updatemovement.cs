﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_game_playerpawn_cl2sv_updatemovement : SBPacket
  {
    public int Unknown;
    public FVector Position;
    public FVector Velocity;
    public byte MoveFrameID;

    protected override void InternalRead (MessageReader reader)
    {
      Unknown = reader.ReadInt32 ();
      Position = reader.ReadFVector ();
      Velocity = reader.ReadFVector ();
      MoveFrameID = reader.ReadByte ();
    }
  }
}
