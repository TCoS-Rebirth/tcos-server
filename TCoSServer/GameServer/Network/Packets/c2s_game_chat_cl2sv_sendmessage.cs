using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_game_chat_cl2sv_sendmessage : SBPacket
  {
    public int CharacterID;
    public byte Channel;
    public string Receiver;
    public string Message;

    protected override void InternalRead (MessageReader reader)
    {
      CharacterID = reader.ReadInt32 ();
      Channel = reader.ReadByte ();
      Receiver = reader.ReadString ();
      Message = reader.ReadString ();
    }
  }
}
