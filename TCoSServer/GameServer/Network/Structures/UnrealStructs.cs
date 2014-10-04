using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct FVector : SBStruct
  {
    public float X; public float Y; public float Z;
    public FVector (float x, float y, float z)
    {
      this.X = x; this.Y = y; this.Z = z;
    }

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (X);
      writer.Write (Y);
      writer.Write (Z);
    }
  }

  struct FRotator : SBStruct
  {
    public int Yaw; public int Pitch; public int Roll;
    public FRotator (int yaw, int pitch, int roll)
    {
      this.Yaw = yaw; this.Pitch = pitch; this.Roll = roll;
    }

    public static FRotator Decompress (int CompressedRotator)
    {
      FRotator DecompressedRotator = new FRotator();
      DecompressedRotator.Pitch = CompressedRotator <<8;
      DecompressedRotator.Yaw = (2 * (CompressedRotator - (32768 * DecompressedRotator.Pitch)));
      DecompressedRotator.Pitch *= 2;
      DecompressedRotator.Roll = ((CompressedRotator << 8) | CompressedRotator);
      return DecompressedRotator;
    }

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Yaw);
      writer.Write (Pitch);
      writer.Write (Roll);
    }
  }
}
