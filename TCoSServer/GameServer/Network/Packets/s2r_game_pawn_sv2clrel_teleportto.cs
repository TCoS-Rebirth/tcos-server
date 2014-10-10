using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Network.Structures;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2r_game_pawn_sv2clrel_teleportto : SBPacket
  {
    public int RelevanceID;
    public FVector NewLocation;
    public FRotator NewRotation;

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2R_GAME_PAWN_SV2CLREL_TELEPORTTO))
      {
        writer.Write (RelevanceID);
        writer.Write (NewLocation);
        writer.Write (NewRotation);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
