using System;
using System.Collections.Generic;

namespace TCoSServer.Common
{
  class MessagePool
  {
    private static MessagePool Singleton = new MessagePool();
    private Queue<Message> availableMessages;
    
    public static MessagePool GetInstance()
    {
      return Singleton;
    }

    private MessagePool()
    {
      availableMessages = new Queue<Message>(500);
    }

    public Message GetMessage()
    {
      Message result = null;
      //Try to get an available message
      try
      {
        result = availableMessages.Dequeue();
        result.reset();
        //If there is no available message, allocate a new one
      } catch (InvalidOperationException)
      {
        result = new Message();
      }
      return result;
    }

    public void ReturnToPool(Message messageToFree)
    {
      availableMessages.Enqueue(messageToFree);
    }
  }
}
