using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TCoSServer.GameServer.Network.Structures;

namespace TCoSServer.Common
{
  /// <summary>
  /// Class helper to write a Message to send it
  /// to the network.
  /// 
  /// Do not hesitate to add helper methods for data types
  /// not handled yet.
  /// </summary>
  public class MessageWriter : IDisposable
  {
    private MemoryStream bodyBuffer;
    private MemoryStream headerBuffer;
    private BinaryWriter headerWriter;
    private BinaryWriter bodyWriter;
    private Message message;

    #region LoginMessage related constructors
    public MessageWriter (Login.LoginMessageId messageId, int initialCapacity)
    {
      headerBuffer = new MemoryStream (Message.headerSize);
      bodyBuffer = new MemoryStream (initialCapacity);
      message = new Message ();
      headerWriter = new BinaryWriter (headerBuffer, Encoding.Unicode);
      bodyWriter = new BinaryWriter (bodyBuffer, Encoding.Unicode);

      SetMessageId (messageId);
    }

    public MessageWriter (Login.LoginMessageId messageId)
      : this (messageId, 20)
    { }

    #endregion 

    #region GameMessage related constructors
    public MessageWriter (GameServer.Network.GameMessageIds messageId, int initialCapacity)
    {
      headerBuffer = new MemoryStream (Message.headerSize);
      bodyBuffer = new MemoryStream (initialCapacity);
      message = new Message ();
      headerWriter = new BinaryWriter (headerBuffer, Encoding.Unicode);
      bodyWriter = new BinaryWriter (bodyBuffer, Encoding.Unicode);

      SetMessageId (messageId);
    }

    public MessageWriter (GameServer.Network.GameMessageIds messageId)
      : this (messageId, 20)
    { }

    #endregion

    private void SetMessageId (Login.LoginMessageId id)
    {
      message.id = (ushort)id;

      {
        headerWriter.Write ((ushort)id);
      }
    }

    private void SetMessageId (GameServer.Network.GameMessageIds id)
    {
      message.id = (ushort)id;

      {
        headerWriter.Write ((ushort)id);
      }
    }

    /// <summary>
    /// Compute the message, and put everything
    /// in data so you can send the message in only one
    /// call to Send. So do not use the header buffer :)
    /// </summary>
    /// <returns></returns>
    public Message ComputeMessage ()
    {
      System.Diagnostics.Debug.Assert (message.id != 0);

      message.data = bodyBuffer.ToArray ();
      message.size = (ushort)message.data.Length;
      headerWriter.Write ((ushort)message.data.Length);
      message.header = headerBuffer.ToArray ();

      headerWriter.Dispose ();
      bodyWriter.Dispose ();
      message.data = message.header.Concat (message.data).ToArray ();
      return message;
    }

    public void Write (int data)
    {
      bodyWriter.Write (data);
    }

    public void Write (uint data)
    {
      bodyWriter.Write (data);

    }

    public void Write (byte data)
    {
      bodyWriter.Write (data);
    }

    public void Write (UInt16 data)
    {
      bodyWriter.Write (data);
    }

    public void Write (float data)
    {
      bodyWriter.Write (data);
    }

    public void Write (string data)
    {
      int stringSize = data.Length;//unicode
      bodyWriter.Write (stringSize);
      bodyWriter.Write (data.ToCharArray ());

    }

    public void Write (MemoryStream stream)
    {
      bodyWriter.Write (stream.GetBuffer (), 0, (int)stream.Length);
    }

    public void Write (SBStruct sbStruct)
    {
      sbStruct.WriteTo (this);
    }

    public void Write<T> (T[] array) where T : SBStruct
    {
      if (array == null)
      {
        bodyWriter.Write (0);
        return;
      }

      bodyWriter.Write (array.Length);
      foreach (SBStruct data in array)
      {
        data.WriteTo (this);
      }
    }

    /// <summary>
    /// Write a byte array without prefixing it with its size.
    /// </summary>
    /// <param name="array"></param>
    public void WriteRawBytes (byte[] array)
    {
      bodyWriter.Write (array);
    }

    public void Dispose ()
    {
      headerWriter.Dispose ();
      bodyWriter.Dispose ();
    }
  }
}
