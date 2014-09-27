using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using TCoSServer.Common;
using System.Diagnostics;

namespace TCoSServer
{
  namespace Login
  {
    public class LoginServer
    {

      private ManualResetEvent listenForNewConnection = new ManualResetEvent (false);      
      private Socket listener;
      private IPAddress ipAddress;
      private int port;
      private bool valid;

      public bool isValid
      {
        get { return valid; }
      }

      public LoginServer (string loginIp, string loginPort)
      {
        valid = true;

        try
        {
          ipAddress = IPAddress.Parse (loginIp);
          port = Int32.Parse (loginPort);
        }
        catch (FormatException)
        {
          valid = false;
        }
        
      }

      public void StopListening ()
      {
        listener.Close ();
        Console.WriteLine ("Login server stopped");
      }

      public void StartListening ()
      {
        if (!isValid)
          throw new Exception ("Login server is in a invalid state");
        IPEndPoint localEndPoint = new IPEndPoint (ipAddress, port);

        // Create a TCP/IP socket.
        listener = new Socket (AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
          listener.Bind (localEndPoint);
          listener.Listen (100);
   
          while (true)
          {
            if (Thread.CurrentThread.ThreadState == System.Threading.ThreadState.StopRequested)
              break;

            listenForNewConnection.Reset ();

            // Start an asynchronous socket to listen for connections.
            Console.WriteLine ("Waiting for a connection...");
            listener.BeginAccept (
                new AsyncCallback (AcceptNextLoginSession),
                 listener);

            // Wait until a connection is made before continuing.
            listenForNewConnection.WaitOne ();
          }

        }
        catch (Exception e)
        {
          Console.WriteLine (e.ToString ());
        }
      }

      public void AcceptNextLoginSession (IAsyncResult ar)
      {
        // Signal the main thread to continue.
        listenForNewConnection.Set ();

        Socket listener = (Socket)ar.AsyncState;
        Socket sessionHandler = null;
        try
        {
          sessionHandler = listener.EndAccept (ar);
        }
        catch (ObjectDisposedException)
        {
          return;
        }

        LoginSession.StartSession (sessionHandler);
      }

    }//!class LoginServer

  }//!namespace loginserver
}//!namespace TCoSServer

