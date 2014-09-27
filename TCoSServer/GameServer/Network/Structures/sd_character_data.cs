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
  [StructLayout (LayoutKind.Sequential)]
  struct FVector
  {
    public float x; public float y; public float z;
    public FVector (float x, float y, float z)
    {
      this.x = x; this.y = y; this.z = z;
    }
  }

  [StructLayout (LayoutKind.Sequential)]
  struct FRotator
  {
    public int yaw; public int pitch; public int roll;
    public FRotator (int yaw, int pitch, int roll)
    {
      this.yaw = yaw; this.pitch = pitch;  this.roll = roll; 
    }
  }

  struct sd_character_data : SBStruct
  {
    public static readonly int MAX_SIZE = 89;
    public byte Dead;
    public int AccountId;  
    public string Name;
    public FVector Position;
    public int WorldId;
    public int Money;
    public int AppearancePart1;
    public int AppearancePart2;
    public FRotator Rotator;
    public int FactionId;
    public int LastUsedTimeStamp;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Dead);
      writer.Write (AccountId);
      writer.Write (Name);
      writer.Write (Position.x);
      writer.Write (Position.y);
      writer.Write (Position.z);
      writer.Write (WorldId);
      writer.Write (Money);
      writer.Write (AppearancePart1);
      writer.Write (AppearancePart2);
      writer.Write (Rotator.yaw);
      writer.Write (Rotator.pitch);
      writer.Write (Rotator.roll);
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
      writer.Write (Position.x);
      writer.Write (Position.y);
      writer.Write (Position.z);
      writer.Write (WorldId);
      writer.Write (Money);
      writer.Write (AppearancePart1);
      writer.Write (AppearancePart2);
      writer.Write (Rotator.yaw);
      writer.Write (Rotator.pitch);
      writer.Write (Rotator.roll);
      writer.Write (FactionId);
      writer.Write (LastUsedTimeStamp);
      writer.Flush();
      
      return stream;
    }
  }
}
