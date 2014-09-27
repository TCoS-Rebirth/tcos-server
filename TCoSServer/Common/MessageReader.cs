using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TCoSServer.Common
{
  class MessageReader : IDisposable
  {
    private Message message;
    private BinaryReader headerReader;
    private BinaryReader bodyReader;
    private MemoryStream headerBuffer;
    private MemoryStream bodyBuffer;

    private bool headerAnalyzed;

    public MessageReader (Message messageToRead)
    {
      headerAnalyzed = false;
      message = messageToRead;

      if (message.data == null)
      {
        headerBuffer = new MemoryStream (message.header);
        headerReader = new BinaryReader (headerBuffer, Encoding.Unicode);
        //WARNING: MUST BE DONE BEFORE INITIALIZING BODY STUFF
        AnalyzeHeader ();
      }

      if (message.size > 0)
      {
        bodyBuffer = new MemoryStream (message.data);
        bodyReader = new BinaryReader (bodyBuffer, Encoding.Unicode);
      }
    }

    private void AnalyzeHeader ()
    {
      if (headerAnalyzed)
        return;
      message.id = headerReader.ReadUInt16 ();
      message.size = headerReader.ReadUInt16 ();
      message.data = new byte[message.size];

      headerAnalyzed = true;
      headerReader.Dispose ();
    }

    public byte ReadByte ()
    {
      return bodyReader.ReadByte ();
    }

    public char ReadChar ()
    {
      return bodyReader.ReadChar ();
    }

    public uint ReadUInt32 ()
    {
      return bodyReader.ReadUInt32 ();
    }

    public int ReadInt32 ()
    {
      return bodyReader.ReadInt32 ();
    }

    public string ReadString ()
    {
      uint charCount = bodyReader.ReadUInt32 ();
      StringBuilder builder = new StringBuilder ();
      for (uint i = 0; i < charCount; ++i)
        builder.Append (bodyReader.ReadChar ());

      return builder.ToString();
    }

    public byte[] ReadByteArray ()
    {
      int arraySize = bodyReader.ReadInt32 ();
      byte[] array = bodyReader.ReadBytes (arraySize);

      return array;
    }

    public void Dispose ()
    {
      if (!headerAnalyzed && headerReader != null)
        headerReader.Dispose ();

      if (bodyReader != null)
        bodyReader.Dispose ();
    }
  }
}
