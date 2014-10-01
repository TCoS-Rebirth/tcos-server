using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using TCoSServer.Common;
using System.Reflection;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_character_data : SBStruct
  {
    public static readonly int MAX_SIZE = 89;
    public byte Dead;
    public int AccountId;  
    public string Name;
    public FVector Position;
    public int WorldId;
    public int Money;
    public Int32 AppearancePart1;
    public Int32 AppearancePart2;
    public FRotator Rotator;
    public int FactionId;
    public int LastUsedTimeStamp;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Dead);
      writer.Write (AccountId);
      writer.Write (Name);
      writer.Write (Position.X);
      writer.Write (Position.Y);
      writer.Write (Position.Z);
      writer.Write (WorldId);
      writer.Write (Money);
      writer.Write (AppearancePart1);
      writer.Write (AppearancePart2);
      writer.Write (Rotator.Yaw);
      writer.Write (Rotator.Pitch);
      writer.Write (Rotator.Roll);
      writer.Write (FactionId);
      writer.Write (LastUsedTimeStamp);
    }

    public MemoryStream getAsStream ()
    {
      MemoryStream stream = new MemoryStream (MAX_SIZE);
      BinaryWriter writer = new BinaryWriter (stream, Encoding.Unicode);
      
      writer.Write (Dead);
      writer.Write (AccountId);
      writer.Write (Name.Length);
      writer.Write (Name.ToCharArray());
      writer.Write (Position.X);
      writer.Write (Position.Y);
      writer.Write (Position.Z);
      writer.Write (WorldId);
      writer.Write (Money);
      writer.Write (AppearancePart1);
      writer.Write (AppearancePart2);
      writer.Write (Rotator.Yaw);
      writer.Write (Rotator.Pitch);
      writer.Write (Rotator.Roll);
      writer.Write (FactionId);
      writer.Write (LastUsedTimeStamp);
      writer.Flush();
      
      return stream;
    }
  }
}
