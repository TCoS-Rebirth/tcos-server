using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_game_chat_sv2cl_onmessage : SBPacket
  {
    public string Sender;
    public string Message;
    public int Unknown;

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_GAME_CHAT_SV2CL_ONMESSAGE))
      {
        writer.Write (Sender);
        writer.Write (Message);
        writer.Write (Unknown);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
