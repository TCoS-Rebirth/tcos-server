using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_game_actor_move : SBPacket
  {
    public int Unknown1;
    public int Unknown2;//I think it is relevance ID
    public FVector NetLocation;
    public FVector NetRotation;

    public override Message Generate()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_GAME_ACTOR_MOVE))
      {
        writer.Write (Unknown1);
        writer.Write (Unknown2);
        writer.Write (NetLocation);
        writer.Write (NetRotation);

        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
