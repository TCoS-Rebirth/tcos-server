using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2r_game_playerpawn_move : SBPacket
  {
    public int RelevanceID;
    public int MoveFrameID;
    public FVector NetLocation;
    public FVector NetVelocity;
    public byte Physics;

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2R_GAME_PLAYERPAWN_MOVE))
      {
        writer.Write (RelevanceID);
        writer.Write (MoveFrameID);
        writer.Write (NetLocation);
        writer.Write (NetVelocity);
        writer.Write (Physics);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
