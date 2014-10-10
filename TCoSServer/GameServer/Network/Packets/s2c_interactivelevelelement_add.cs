using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Network.Structures;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_interactivelevelelement_add : SBPacket
  {
    public int RelevanceID;
    public int LevelObjectID;
    public int IsEnabledBitfield;
    public int IsHidden;
    public FVector NetLocation;
    public FVector NetRotation;
    public byte CollisionType;
    public int ActivatedRespawnTimerBitfield;//IsActivated+NetIsActivated+RespawnTimerActive

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter(GameMessageIds.S2C_INTERACTIVELEVELELEMENT_ADD))
      {
        writer.Write (RelevanceID);
        writer.Write (LevelObjectID);
        writer.Write (IsEnabledBitfield);
        writer.Write (IsHidden);
        writer.Write (NetLocation);
        writer.Write (NetRotation);
        writer.Write (CollisionType);
        writer.Write (ActivatedRespawnTimerBitfield);
        result = writer.ComputeMessage ();
      }
      return result;
    }
  }
}
