using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Network.Structures;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_game_playerpawn_cl2sv_updaterotation : SBPacket
  {
    public int CharacterID;
    public int CompressedRotator;
    public FRotator DecompressedRotator;

    protected override void InternalRead (MessageReader reader)
    {
      CharacterID = reader.ReadInt32 ();
      CompressedRotator = reader.ReadInt32 ();
    }
  }
}
