using TCoSServer.Common;
using System.Net.Sockets;

namespace TCoSServer.GameServer.Network.Packets
{
  /// <summary>
  /// Base class for packet handling.
  /// The InternalRead is not abstract because the server messages
  /// does not need to implement it (generally).
  /// The same for Generate.
  /// This class allows you to simply write something like:
  /// <code>
  /// packet_type packet = new packet_type();
  /// packet.ReadFrom (message);//message is a Common.Message just received from the net
  /// </code>
  /// or like
  /// <code>
  /// packet_type packet = new packet_type();
  /// packet.Field1 = xxx;
  /// packet.Field2 = xxx;
  /// Message message = packet.Generate();
  /// socket.Send (message.data, message.size, SocketFlags.None);
  /// </code>
  /// </summary>
  class SBPacket
  {
    /// <summary>
    /// Finish to receive the message data from the network and then
    /// fill the fields of the SBPacket according to its actual type.
    /// </summary>
    /// <param name="message">The message just received from the network with the header already received and parsed.
    /// For instance before calling this method you should use a MessageReader to parse the header.
    /// Actually this should have been already done by the message handling loop.
    /// <see cref="NetworkPlayer"/> for an example of use.</param>
    public void ReadFrom (Message message)
    {
      if (message.size > 0)
        message.clientSocket.Receive (message.data, message.size, System.Net.Sockets.SocketFlags.None);
      using (MessageReader reader = new MessageReader (message))
      {
        InternalRead (reader);
      }
    }

    //Implement it in subclasses in order to generate a correct message
    public virtual Message Generate ()
    {
      return null;
    }

    //Implement it in subclasses in order to parse the message
    protected virtual void InternalRead (MessageReader reader)
    { }
  }
}
