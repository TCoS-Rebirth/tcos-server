using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_game_playerpawn_sv2cl_clientsidetrigger : SBPacket
  {
    public String EventTrigger;
    public int RelevanceId;

    public override Message Generate()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter(GameMessageIds.S2C_GAME_PLAYERPAWN_SV2CL_CLIENTSIDETRIGGER))
      {
        writer.Write(EventTrigger);
        writer.Write(RelevanceId);
        result = writer.ComputeMessage();
      }
      return result;
    }
  }
}
