using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TCoSServer.Common
{
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
  }

  public delegate void HandleMessageCallback (Message m);

}
