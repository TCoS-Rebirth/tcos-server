using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_pawn_stream : SBStruct
  {
    public int Unknown1;
    public int Unknown2;
    public int Unknown3;
    public int Unknown4;
    public int Unknown5;
    public int Unknown6;
    public byte PhysicType;//1 = walking/normal, 4= flying...
    public byte Unknown7;
    public byte PawnState;//ePawnState { NONE = 0, ALIVE = 1, DEATH = 2 }
    public int Unknown8;
    public float BaseMoveSpeed; //2 = normal
    public int Unknown9;//some kind of flags (550 = green arrow (latency simulation maybe?)
    public int Visibility;// {visible = 0, invisible = 1}

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Unknown1);
      writer.Write (Unknown2);
      writer.Write (Unknown3);
      writer.Write (Unknown4);
      writer.Write (Unknown5);
      writer.Write (Unknown6);
      writer.Write (PhysicType);
      writer.Write (Unknown7);
      writer.Write (PawnState);
      writer.Write (Unknown8);
      writer.Write (BaseMoveSpeed);
      writer.Write (Unknown9);
      writer.Write (Visibility);
    }
  }
}
