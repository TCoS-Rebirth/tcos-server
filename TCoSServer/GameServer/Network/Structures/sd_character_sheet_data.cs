using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  class sd_character_sheet_data : SBStruct
  {
    public static readonly int Size = 24;
    public int ClassId;
    public float FamePoints;
    public float PepPoints;
    public float Health;
    public int SelectedSkillDeckID;
    public byte ExtraBodyPoints;
    public byte ExtraMindPoints;
    public byte ExtraFocusPoints;
    public byte UnknownByte;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (ClassId);
      writer.Write (FamePoints);
      writer.Write (PepPoints);
      writer.Write (Health);
      writer.Write (SelectedSkillDeckID);
      writer.Write (ExtraBodyPoints);
      writer.Write (ExtraMindPoints);
      writer.Write (ExtraFocusPoints);
      writer.Write (UnknownByte);
    }
  }
}
