using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TCoSServer.Common
{
  /// <summary>
  /// Class containing low level data about a network message.
  /// When the Message is read from the network, the data field
  /// contains only the actual message data and not the header.
  /// When the Message is written by the application (with MessageWriter),
  /// the data field contains also the header so you can send the message
  /// in only one call to Socket.Send()
  /// </summary>
  public class Message
  {
    // Client  socket.
    public Socket clientSocket = null;
    // Header size is always the same
    public const int headerSize = DataType.DWORD;
    // Receive buffer.
    public byte[] header = new byte[headerSize];
    public UInt16 id;
    //Defined by the header
    public UInt16 size = 0;
    public byte[] data = null;

    public void reset()
    {
      data = null;
      size = 0;
      id = 0;
      clientSocket = null;
      header = null;
      header = new byte[headerSize];
    }
  }

  /// <summary>
  /// Simple delegate you can use for your message handling code.
  /// </summary>
  /// <param name="m"></param>
  public delegate void HandleMessageCallback (Message m);

}
