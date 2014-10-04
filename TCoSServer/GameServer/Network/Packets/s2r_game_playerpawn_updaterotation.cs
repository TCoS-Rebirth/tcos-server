using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2r_game_playerpawn_updaterotation : SBPacket
  {
    public int RelevanceID;
    public int CompressedRotator;

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2R_GAME_PLAYERPAWN_UPDATEROTATION))
      {
        writer.Write (RelevanceID);
        writer.Write (CompressedRotator);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
