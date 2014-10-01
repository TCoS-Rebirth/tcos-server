using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_game_playerpawn_updatemovementwithpysics : SBPacket
  {
    public int Unknown;
    public FVector Position;
    public FVector Direction;
    public byte Physics;
    public byte FrameId;

    protected override void InternalRead (MessageReader reader)
    {
      Unknown = reader.ReadInt32 ();
      Position = reader.ReadFVector ();
      Direction = reader.ReadFVector ();
      Physics = reader.ReadByte ();
      FrameId = reader.ReadByte ();
    }
  }
}
