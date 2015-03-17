using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageExtractor
{
  class SBFileReader : IDisposable
  {

    public long Position { get { return currentOffset; } }

    private MemoryMappedFile file;
    private MemoryMappedViewAccessor accessor;
    private long currentOffset;
    private FileInfo info;

    public SBFileReader (string fullFilePath)
    {
      file = MemoryMappedFile.CreateFromFile (fullFilePath, FileMode.Open);
      info = new FileInfo (fullFilePath);
      accessor = file.CreateViewAccessor (0, 0);
      currentOffset = 0;
    }

    public void Seek (long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          currentOffset = offset;
          break;
        case SeekOrigin.Current:
          currentOffset += offset;
          break;
        case SeekOrigin.End:
          currentOffset = info.Length - offset;
          break;
      }
    }

    public void Read<T> (out T structure, int structSize) where T : struct
    {
      accessor.Read (currentOffset, out structure);
      currentOffset += structSize;
    }

    public byte ReadByte ()
    {
      byte result = accessor.ReadByte (currentOffset);
      currentOffset++;
      return result;
    }

    public byte[] ReadBytes (int count)
    {
      byte[] result = new byte[count];
      accessor.ReadArray<byte> (currentOffset, result, 0, count);
      currentOffset += count;
      return result;
    }

    public Int16 ReadInt16 ()
    {
      Int16 result = accessor.ReadInt16 (currentOffset);
      currentOffset += 2;
      return result;
    }

    public UInt32 ReadUInt32 ()
    {
      UInt32 result = accessor.ReadUInt32 (currentOffset);
      currentOffset += 4;
      return result;

    }

    public Int32 ReadInt32 ()
    {
      Int32 result = accessor.ReadInt32 (currentOffset);
      currentOffset += 4;
      return result;
    }

    public Single ReadFloat ()
    {
      Single result = accessor.ReadSingle (currentOffset);
      currentOffset += 4;
      return result;
    }

    public Int64 ReadInt64 ()
    {
      Int64 result = accessor.ReadInt64 (currentOffset);
      currentOffset += 8;
      return result;
    }

    //Helper function to avoid API break: allows to call ReadIndex without parameters
    public Int32 ReadIndex()
    {
      int dummy = 0;//this var is not used

      int index = ReadIndex(ref dummy);

      return index;
    }

    public Int32 ReadIndex (ref int indexSize)
    {
      bool isNegative = false;
      byte firstByte = ReadByte ();
      indexSize = 1;
      Int32 index = firstByte & 0x3F;
      if ((firstByte & 0x80) != 0)
        isNegative = true;

      if ((firstByte & 0x40) != 0)
      {
        byte secondByte = ReadByte ();
        ++indexSize;
        index = index | ((secondByte & 0x7F) << 6);
        if ((secondByte & 0x80) != 0)
        {
          byte thirdByte = ReadByte ();
          ++indexSize;
          index = index | ((thirdByte & 0x7F) << 13);
          if ((thirdByte & 0x80) != 0)
          {
            byte fourthByte = ReadByte ();
            ++indexSize;
            index = index | ((fourthByte & 0x7f) << 20);
            if ((fourthByte & 0x80) != 0)
            {
              byte fifthByte = ReadByte ();
              ++indexSize;
              index = index | ((fifthByte & 0x7F) << 27);
            }
          }
        }
      }
      if (isNegative)
        index = -index;

      return index;
    }

    public void Dispose ()
    {
      accessor.Dispose ();
      file.Dispose ();
    }
  }
}
