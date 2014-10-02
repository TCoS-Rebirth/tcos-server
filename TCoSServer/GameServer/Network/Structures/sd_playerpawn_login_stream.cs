using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_playerpawn_login_stream : SBStruct
  {
    public FVector NetVelocity;
    public FVector NetLocation;
    public byte PhysicType;//1 = walking/normal, 4= flying...
    public byte MoveFrameID;
    public byte PawnState;//ePawnState { NONE = 0, ALIVE = 1, DEATH = 2 }
    public int BitfieldInvulnerable;
    public float BaseMoveSpeed; //2 = normal
    public int DebugFilters;//some kind of flags (550 = green arrow (latency simulation maybe?)
    public int Visibility;// {visible = 0, invisible = 1}

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (NetVelocity);
      writer.Write (NetLocation);
      writer.Write (PhysicType);
      writer.Write (MoveFrameID);
      writer.Write (PawnState);
      writer.Write (BitfieldInvulnerable);
      writer.Write (BaseMoveSpeed);
      writer.Write (DebugFilters);
      writer.Write (Visibility);
    }
  }
}
