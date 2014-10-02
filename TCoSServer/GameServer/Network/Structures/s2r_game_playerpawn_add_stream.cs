using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct s2r_game_playerpawn_add_stream : SBStruct
  {
    public FVector NetVelocity;
    public FVector NetLocation;
    public byte Physics;
    public byte MoveFrameID;
    public float PvPTimer;
    public byte PawnState;
    public int BitFieldInvulenarbility;//See SBGameClasses.h:4819 in AGame_Pawn class
    public float GroundSpeedModifier;
    public int DebugFilters;
    public int BitFieldHasPet_Invisible_JumpedFromLadder;//idem

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (NetVelocity);
      writer.Write (NetLocation);
      writer.Write (Physics);
      writer.Write (MoveFrameID);
      writer.Write (PvPTimer);
      writer.Write (PawnState);
      writer.Write (BitFieldInvulenarbility);
      writer.Write (GroundSpeedModifier);
      writer.Write (DebugFilters);
      writer.Write (BitFieldHasPet_Invisible_JumpedFromLadder);
    }
  }
}
